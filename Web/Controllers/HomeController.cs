using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Interfaces.UOW;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly BaseIUOW _uow;

        public HomeController(BaseIUOW uow)
        {
            _uow = uow;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}