using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Web.Mvc;
using API_DAL.Interfaces;
using BLL.Helpers.Factories;
using BLL.ViewModels.Vehicle;
using Domain;
using Domain.Identity;
using Interfaces.UOW;
using Microsoft.AspNet.Identity;
using PagedList;
using Santhos.Web.Mvc.BootstrapFlashMessages;
using Web.Controllers;

namespace Web.Areas.MemberArea.Controllers
{
    [Authorize (Roles = "User")]
    public class VehiclesController : BaseController
    {
        private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly string _instanceId = Guid.NewGuid().ToString();

        private readonly BaseIUOW _uow;
        private ApplicationUserManager _userManager;

        public VehiclesController(BaseIUOW uow, ApplicationUserManager userManager)
        {
            _logger.Debug("InstanceId: " + _instanceId);
            _uow = uow;
            _userManager = userManager;
        }

        // GET: Vehicles
        public ActionResult Index(IndexModel vm)
        {
            int totalVehicleCount;
            string realSortProperty;

            // if not set, set base values
            vm.PageNumber = vm.PageNumber ?? 1;
            vm.PageSize = vm.PageSize ?? 25;

            var res = _uow.GetRepository<IVehicleRepository>().GetUserVehicleList(vm.SortProperty, vm.PageNumber.Value - 1, vm.PageSize.Value, out totalVehicleCount, out realSortProperty);

            vm.SortProperty = realSortProperty;

            // https://github.com/kpi-ua/X.PagedList
            vm.Vehicles = new StaticPagedList<IndexVehicleModel>(res, vm.PageNumber.Value, vm.PageSize.Value, totalVehicleCount);

            return View(vm);
        }

        // GET:Vehicles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailsModel vehicle = _uow.GetRepository<IVehicleRepository>().GetUserVehicle(id.Value);
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
            try
            {

                if (ModelState.IsValid == false)
                {
                    throw new ValidationException("Invalid model state");
                }
                _uow.GetRepository<IVehicleRepository>().AddVehicle(vehicleCreateModel);
                this.FlashSuccess("Vehicle created");
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                this.FlashDanger("There were errors on form");
                return View(vehicleCreateModel);
            }
        }

        // GET: Vehicles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailsModel vehicle = _uow.GetRepository<IVehicleRepository>().GetUserVehicle(id.Value);
            if (vehicle == null)
            {
                return HttpNotFound();
            }

            return View(UpdateModelFactory.CreateFromDetailsModel(vehicle));
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
            DetailsModel vehicle = _uow.GetRepository<IVehicleRepository>().GetUserVehicle(id.Value);
            if (vehicle == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.GetRepository<IVehicleRepository>().UpdateVehicle(vehicle.Id, vehicleUpdateModel);
                this.FlashSuccess("Vehicle edited");
                return RedirectToAction("Index");
            }
            this.FlashDanger("There were errors on form");
            return View(vehicleUpdateModel);
        }

        // GET: Vehicles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailsModel vehicle = _uow.GetRepository<IVehicleRepository>().GetUserVehicle(id.Value);

            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // vehicle grants access to all sub objects
            DetailsModel vehicle = _uow.GetRepository<IVehicleRepository>().GetUserVehicle(id);

            if (vehicle == null)
            {
                return HttpNotFound();
            }
            
            _uow.GetRepository<IVehicleRepository>().Delete(vehicle.Id);
            
            this.FlashSuccess("Vehicle deleted");
            return RedirectToAction("Index");
        }
    }
}
