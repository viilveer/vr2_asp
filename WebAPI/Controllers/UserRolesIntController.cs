using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DAL;
using Domain;
using Domain.Identity;
using Interfaces.UOW;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/UserRolesInt")]
    public class UserRolesIntController : ApiController
    {
        //private DataBaseContext db = new DataBaseContext();

        private readonly BaseIUOW _uow;

        public UserRolesIntController(BaseIUOW uow)
        {
            _uow = uow;

        }



        [ResponseType(typeof(UserRoleInt))]
        public IHttpActionResult PostUserRoleInt(UserRoleInt userRoleInt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _uow.UserRolesInt.Add(userRoleInt);
            _uow.Commit();

            return CreatedAtRoute("DefaultApi", new { id = userRoleInt.RoleId }, userRoleInt);
        }

      

        protected override void Dispose(bool disposing)
        {
        }


    }
}