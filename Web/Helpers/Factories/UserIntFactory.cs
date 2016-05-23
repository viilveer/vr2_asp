using System;
using System.Security.Principal;
using DAL;
using DAL.Interfaces;
using Domain.Identity;
using Interfaces.Repositories;
using Interfaces.UOW;
using Microsoft.AspNet.Identity;

namespace Web.Helpers.Factories
{
    public class UserIntFactory
    {
        public static UserInt CreateFromIdentity(BaseIUOW uow, IPrincipal identity)
        {
            int userId = Convert.ToInt32(identity.Identity.GetUserId());
            return uow.GetRepository<IUserIntRepository>().GetById(userId);
        }
    }
}