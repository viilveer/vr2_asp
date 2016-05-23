using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Interfaces.Repositories;
using Domain.Identity;
using Microsoft.Owin.Security;

namespace API_DAL.Repositories
{
    public class UserClaimIntRepository :
        UserClaimRepository<int, RoleInt, UserInt, UserClaimInt, UserLoginInt, UserRoleInt>, IUserClaimIntRepository
    {
        public UserClaimIntRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
        {
        }
    }

    public class UserClaimRepository : UserClaimRepository<string, Role, User, UserClaim, UserLogin, UserRole>,
        IUserClaimRepository
    {
        public UserClaimRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
        {
        }
    }

    public class UserClaimRepository<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole> : ApiRepository<TUserClaim>
        where TKey : IEquatable<TKey>
        where TRole : Role<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUser : User<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserClaim : UserClaim<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserLogin : UserLogin<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserRole : UserRole<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
    {
        public UserClaimRepository(HttpClient httpClient, string endPoint, IAuthenticationManager authenticationManager) : base(httpClient, endPoint, authenticationManager)
        {
        }

        public List<TUserClaim> AllIncludeUser()
        {
            throw new NotImplementedException();
        }
        //public List<TUserClaim> AllIncludeUser()
        //{
        //    return DbSet.Include(a => a.User).ToList();
        //}
    }
}