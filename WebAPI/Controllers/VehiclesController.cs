using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL.Helpers.Factories;
using BLL.ViewModels.Vehicle;
using DAL.Interfaces;
using Domain;
using Domain.Identity;
using Interfaces.Repositories;
using Microsoft.AspNet.Identity;
using NLog;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/UserVehicles")]
    public class VehiclesController : ApiController
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly string _instanceId = Guid.NewGuid().ToString();

        private readonly IUOW _uow;
        private ApplicationUserManager _userManager;

        public VehiclesController(IUOW uow, ApplicationUserManager userManager)
        {
            _logger.Debug("InstanceId: " + _instanceId);
            _uow = uow;
            _userManager = userManager;
        }
        /// <summary>
        /// Retrieves user vehicle list, supports paginating
        /// </summary>
        [HttpGet]
        [Route("")]
        public HttpResponseMessage Index(string sortProperty, int pageNumber, int pageSize)
        {
            int totalCount;
            string realSortProperty;

            VehicleService service = new VehicleService(_uow.GetRepository<IVehicleRepository>());
            List<IndexVehicleModel> vehicles = service.GetUserVehiclesList(User.Identity.GetUserId<int>(), sortProperty,
                pageNumber, pageSize, out totalCount, out realSortProperty);

            var response = Request.CreateResponse(HttpStatusCode.OK, vehicles);

            // Set headers for paging
            response.Headers.Add("X-Paging-PageNo", pageNumber.ToString());
            response.Headers.Add("X-Paging-PageSize", pageSize.ToString());
            response.Headers.Add("X-RealSortProperty", realSortProperty);
            response.Headers.Add("X-Paging-TotalRecordCount", totalCount.ToString());

            return response;
        }

        /// <summary>
        /// Retrieves user single vehicle
        /// </summary>
        [HttpGet]
        [Route("{vehicleId}")]
        public DetailsModel UserVehicle(int vehicleId)
        {
            VehicleService service = new VehicleService(_uow.GetRepository<IVehicleRepository>());
            return service.GetUserVehicle(User.Identity.GetUserId<int>(), vehicleId);
        }
        /// <summary>
        /// Creates new user vehicle
        /// </summary>
        [HttpPost]
        [Route("")]
        public IHttpActionResult Create(CreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

          
            UserInt user = UserIntFactory.CreateFromIdentity(_uow, User);
            Vehicle vehicle = model.GetVehicle(user);
            _uow.GetRepository<IVehicleRepository>().Add(vehicle);
            _uow.Commit();
            _uow.RefreshAllEntities();

            Blog blog = new Blog
            {
                Vehicle = vehicle,
                VehicleId = vehicle.VehicleId,
                AuthorId = user.Id,
                Name = vehicle.Make + " " + vehicle.Model // TODO :: ugly
            };

            _uow.GetRepository<IBlogRepository>().Add(blog);
            _uow.Commit();

            if (_userManager.IsInRole(User.Identity.GetUserId<int>(), "CarOwner") == false)
            {
                _userManager.AddToRole(User.Identity.GetUserId<int>(), "CarOwner");
            }

            return Ok(model);
        }
        /// <summary>
        /// Updates user vehicle
        /// </summary>
        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Update(int id, UpdateModel updateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Vehicle vehicle = _uow.GetRepository<IVehicleRepository>().GetByIdAndUserId(id, User.Identity.GetUserId<int>());
            if (vehicle == null)
            {
                return BadRequest(ModelState);
            }

            _uow.GetRepository<IVehicleRepository>().Update(updateModel.UpdateVehicle(vehicle));
            _uow.Commit();


            return Ok(updateModel);
        }
        /// <summary>
        /// Deletes user vehicle
        /// </summary>
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            Vehicle vehicle = _uow.GetRepository<IVehicleRepository>().GetByIdAndUserId(id, User.Identity.GetUserId<int>());
            if (vehicle != null)
            {
                if (vehicle.Blog != null)
                {
                    _uow.GetRepository<IBlogPostRepository>().DeleteByBlogId(vehicle.Blog.BlogId);
                    _uow.GetRepository<IUserBlogConnectionRepository>().DeleteByBlogId(vehicle.Blog.BlogId);
                    _uow.GetRepository<IBlogRepository>().Delete(vehicle.Blog.BlogId);
                }

                _uow.GetRepository<IVehicleRepository>().Delete(vehicle.VehicleId);

                _uow.Commit();
                return Ok();
            }
            return BadRequest();

        }



        //// POST: Vehicles/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Make,Model,Year,Kw,Engine")] CreateModel vehicleCreateModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _uow.BeginTransaction();
        //        try
        //        {


        //            if (_userManager.IsInRole(user.Id, "CarOwner") == false)
        //            {
        //                _userManager.AddToRole(user.Id, "CarOwner");
        //            }
        //            _uow.CommitTransaction();


        //        }
        //        catch (Exception ex)
        //        {
        //            _uow.RollbackTransaction();
        //            throw ex;
        //        }
        //        this.FlashSuccess("Vehicle created");

        //        return RedirectToAction("Index");
        //    }
        //    this.FlashDanger("There were errors on form");
        //    return View(vehicleCreateModel);
        //}

        //// GET: Vehicles/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Vehicle vehicle = _uow.GetRepository<IVehicleRepository>().GetById(id);
        //    if (vehicle == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return View(UpdateModelFactory.CreateFromVehicle(vehicle));
        //}

        //// POST: Vehicles/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int? id, [Bind(Include = "VehicleId,Make,Model,Year,Kw,Engine")] UpdateModel vehicleUpdateModel)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Vehicle vehicle = _uow.GetRepository<IVehicleRepository>().GetById(id);
        //    if (vehicle == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        _uow.GetRepository<IVehicleRepository>().Update(vehicleUpdateModel.UpdateVehicle(vehicle));
        //        _uow.Commit();
        //        this.FlashSuccess("Vehicle edited");
        //        return RedirectToAction("Index");
        //    }
        //    this.FlashDanger("There were errors on form");
        //    return View(vehicleUpdateModel);
        //}

        //// GET: Vehicles/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Vehicle vehicle = _uow.GetRepository<IVehicleRepository>().GetById(id);

        //    if (vehicle == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(vehicle);
        //}

        //// POST: Vehicles/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    // vehicle grants access to all sub objects
        //    Vehicle vehicle = _uow.GetRepository<IVehicleRepository>()
        //        .GetByIdAndUserId(id, User.Identity.GetUserId<int>());

        //    if (vehicle == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    _uow.Commit();

        //    this.FlashSuccess("Vehicle deleted");
        //    return RedirectToAction("Index");
        //}
    }
}
