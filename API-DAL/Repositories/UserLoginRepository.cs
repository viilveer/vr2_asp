using System;
using System.Collections.Generic;
using System.Linq;
using API_DAL.Interfaces;
using Domain.Identity;

namespace API_DAL.Repositories
{
    public class UserLoginIntRepository :
        UserLoginRepository<int, RoleInt, UserInt, UserClaimInt, UserLoginInt, UserRoleInt>, IUserLoginIntRepository
    {
    }

    public class UserLoginRepository : UserLoginRepository<string, Role, User, UserClaim, UserLogin, UserRole>,
        IUserLoginRepository
    {
       
    }

    public class UserLoginRepository<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole> : ApiRepository<TUserLogin>
        where TKey : IEquatable<TKey>
        where TRole : Role<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUser : User<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserClaim : UserClaim<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserLogin : UserLogin<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserRole : UserRole<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
    {


        //public List<TUserLogin> GetAllIncludeUser()
        //{
        //    return DbSet.Include(a => a.User).ToList();
        //}

        //public TUserLogin GetUserLoginByProviderAndProviderKey(string loginProvider, string providerKey)
        //{
        //    return DbSet.FirstOrDefault(l => l.LoginProvider == loginProvider && l.ProviderKey == providerKey);
        //}

        public List<TUserLogin> GetAllIncludeUser()
        {
            throw new NotImplementedException();
        }

        public TUserLogin GetUserLoginByProviderAndProviderKey(string loginProvider, string providerKey)
        {
            throw new NotImplementedException();
        }
    }
}