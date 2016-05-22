//using System;
//using System.Net;
//using System.Web.Mvc;
//using DAL.Interfaces;
//using Domain;
//using Domain.Identity;
//using Microsoft.AspNet.Identity;
//using PagedList;
//using Santhos.Web.Mvc.BootstrapFlashMessages;
//using WebAPI.ViewModels.Vehicle;
//using Web.Controllers;
//using Web.Helpers.Factories;

//namespace WebAPI.Controllers
//{
//    [Authorize (Roles = "User")]
//    public class VehiclesController : BaseController
//    {
//        private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
//        private readonly string _instanceId = Guid.NewGuid().ToString();

//        private readonly IUOW _uow;
//        private ApplicationUserManager _userManager;

//        public VehiclesController(IUOW uow, ApplicationUserManager userManager)
//        {
//            _logger.Debug("InstanceId: " + _instanceId);
//            _uow = uow;
//            _userManager = userManager;
//        }

//        // GET: Vehicles
//        public ActionResult Index(IndexModel vm)
//        {
//            int totalVehicleCount;
//            string realSortProperty;

//            // if not set, set base values
//            vm.PageNumber = vm.PageNumber ?? 1;
//            vm.PageSize = vm.PageSize ?? 25;

//            var res = _uow.GetRepository<IVehicleRepository>().GetListByUserId(User.Identity.GetUserId<int>(), vm.SortProperty, vm.PageNumber.Value - 1, vm.PageSize.Value, out totalVehicleCount, out realSortProperty);

//            vm.SortProperty = realSortProperty;

//            // https://github.com/kpi-ua/X.PagedList
//            vm.Vehicles = new StaticPagedList<Vehicle>(res, vm.PageNumber.Value, vm.PageSize.Value, totalVehicleCount);

//            return View(vm);
//        }

//        // GET: Vehicles/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Vehicle vehicle = _uow.GetRepository<IVehicleRepository>().GetById(id);
//            if (vehicle == null)
//            {
//                return HttpNotFound();
//            }
//            return View(vehicle);
//        }

//        // GET: Vehicles/Create
//        public ActionResult Create()
//        {
//            return View();
//        }

//        // POST: Vehicles/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "Make,Model,Year,Kw,Engine")] CreateModel vehicleCreateModel)
//        {
//            if (ModelState.IsValid)
//            {
//               _uow.BeginTransaction();
//                try
//                {
//                    UserInt user = UserIntFactory.CreateFromIdentity(_uow, User);
//                    Vehicle vehicle = vehicleCreateModel.GetVehicle(user);
//                    _uow.GetRepository<IVehicleRepository>().Add(vehicle);

//                    _uow.Commit();
//                    _uow.RefreshAllEntities();
//                    _logger.Debug(vehicle.VehicleId.ToString);
//                    Blog blog = new Blog
//                    {
//                        Vehicle = vehicle,
//                        VehicleId = vehicle.VehicleId,
//                        AuthorId = user.Id,
//                        Name = vehicle.Make + " " + vehicle.Model // TODO :: ugly
//                    };

//                    _uow.GetRepository<IBlogRepository>().Add(blog);
//                    _uow.Commit();

//                    if (_userManager.IsInRole(user.Id, "CarOwner") == false)
//                    {
//                        _userManager.AddToRole(user.Id, "CarOwner");
//                    }
//                    _uow.CommitTransaction();

                   
//                }
//                catch (Exception ex)
//                {
//                    _uow.RollbackTransaction();
//                    throw ex;
//                }
//                this.FlashSuccess("Vehicle created");

//                return RedirectToAction("Index");
//            }
//            this.FlashDanger("There were errors on form");
//            return View(vehicleCreateModel);
//        }

//        // GET: Vehicles/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Vehicle vehicle = _uow.GetRepository<IVehicleRepository>().GetById(id);
//            if (vehicle == null)
//            {
//                return HttpNotFound();
//            }

//            return View(UpdateModelFactory.CreateFromVehicle(vehicle));
//        }

//        // POST: Vehicles/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit(int? id, [Bind(Include = "VehicleId,Make,Model,Year,Kw,Engine")] UpdateModel vehicleUpdateModel)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Vehicle vehicle = _uow.GetRepository<IVehicleRepository>().GetById(id);
//            if (vehicle == null)
//            {
//                return HttpNotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                _uow.GetRepository<IVehicleRepository>().Update(vehicleUpdateModel.UpdateVehicle(vehicle));
//                _uow.Commit();
//                this.FlashSuccess("Vehicle edited");
//                return RedirectToAction("Index");
//            }
//            this.FlashDanger("There were errors on form");
//            return View(vehicleUpdateModel);
//        }

//        // GET: Vehicles/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Vehicle vehicle = _uow.GetRepository<IVehicleRepository>().GetById(id);

//            if (vehicle == null)
//            {
//                return HttpNotFound();
//            }
//            return View(vehicle);
//        }

//        // POST: Vehicles/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            // vehicle grants access to all sub objects
//            Vehicle vehicle = _uow.GetRepository<IVehicleRepository>()
//                .GetByIdAndUserId(id, User.Identity.GetUserId<int>());

//            if (vehicle == null)
//            {
//                return HttpNotFound();
//            }
//            _uow.GetRepository<IBlogPostRepository>().DeleteByBlogId(vehicle.Blog.BlogId);
//            _uow.GetRepository<IUserBlogConnectionRepository>().DeleteByBlogId(vehicle.Blog.BlogId);
//            _uow.GetRepository<IBlogRepository>().Delete(vehicle.Blog.BlogId);
//            _uow.GetRepository<IVehicleRepository>().Delete(vehicle.VehicleId);
//            _uow.Commit();
            
//            this.FlashSuccess("Vehicle deleted");
//            return RedirectToAction("Index");
//        }
//    }
//}
