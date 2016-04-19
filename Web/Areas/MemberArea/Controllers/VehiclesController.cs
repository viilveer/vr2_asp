using System;
using System.Globalization;
using System.Net;
using System.Web.Mvc;
using DAL.Interfaces;
using Domain;
using Domain.Identity;
using Microsoft.AspNet.Identity;
using Web.Areas.MemberArea.ViewModels.vehicle;
using Web.Controllers;

namespace Web.Areas.MemberArea.Controllers
{
    [Authorize (Roles = "User")]
    public class VehiclesController : BaseController
    {
        private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly string _instanceId = Guid.NewGuid().ToString();

        private readonly IUOW _uow;
        private ApplicationUserManager _userManager;

        public VehiclesController(IUOW uow, ApplicationUserManager userManager)
        {
            _logger.Debug("InstanceId: " + _instanceId);
            _uow = uow;
            _userManager = userManager;
        }

        // GET: Vehicles
        public ActionResult Index()
        {
            var vehicles = _uow.GetRepository<IVehicleRepository>().GetAllByUserId(Convert.ToInt32(User.Identity.GetUserId()));
            return View(vehicles);
        }

        // GET: Vehicles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = _uow.GetRepository<IVehicleRepository>().GetById(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // GET: Vehicles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Make,Model,Year,Kw,Engine")] CreateModel vehicleCreateModel)
        {
            if (ModelState.IsValid)
            {
                // TODO :: move to component
                int userId = Convert.ToInt32(User.Identity.GetUserId());
                UserInt user = _uow.GetRepository<IUserIntRepository>().GetById(userId);
                Vehicle vehicle = vehicleCreateModel.GetVehicle(user);
                _uow.GetRepository<IVehicleRepository>().Add(vehicle);
               
                Blog blog = new Blog();
                blog.VehicleId = vehicle.VehicleId;
                blog.Name = new MultiLangString(vehicle.Make + " " + vehicle.Model); // TODO :: ugly
                blog.CreatedAt = DateTime.Now;
                blog.CreatedBy = user.Email;
                _uow.GetRepository<IBlogRepository>().Add(blog);

                if (_userManager.IsInRole(userId, "CarOwner") == false)
                {
                    _userManager.AddToRole(userId, "CarOwner");
                }
                

                _uow.Commit();
                return RedirectToAction("Index");
            }

            return View(vehicleCreateModel);
        }

        // GET: Vehicles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = _uow.GetRepository<IVehicleRepository>().GetById(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }

            return View(UpdateModelFactory.CreateFromVehicle(vehicle));
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, [Bind(Include = "VehicleId,Make,Model,Year,Kw,Engine")] UpdateModel vehicleUpdateModel)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = _uow.GetRepository<IVehicleRepository>().GetById(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                int userId = Convert.ToInt32(User.Identity.GetUserId());
                UserInt user = _uow.GetRepository<IUserIntRepository>().GetById(userId);
                _uow.GetRepository<IVehicleRepository>().Update(vehicleUpdateModel.UpdateVehicle(vehicle, user));
                _uow.Commit();

                return RedirectToAction("Index");
            }
            return View(vehicleUpdateModel);
        }

        // GET: Vehicles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = _uow.GetRepository<IVehicleRepository>().GetById(id);

            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public RedirectToRouteResult DeleteConfirmed(int id)
        {
            _uow.GetRepository<IVehicleRepository>().Delete(id);
            return RedirectToAction("Index");
        }
    }
}
