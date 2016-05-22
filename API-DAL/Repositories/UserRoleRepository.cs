using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_DAL.Interfaces;
using Domain.Identity;

namespace API_DAL.Repositories
{
    public class UserRoleIntRepository :
        UserRoleRepository<int, RoleInt, UserInt, UserClaimInt, UserLoginInt, UserRoleInt>, IUserRoleIntRepository
    {
      
    }

    public class UserRoleRepository : UserRoleRepository<string, Role, User, UserClaim, UserLogin, UserRole>,
        IUserRoleRepository
    {
       
    }

    public class UserRoleRepository<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole> : ApiRepository<TUserRole>
        where TKey : IEquatable<TKey>
        where TRole : Role<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUser : User<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserClaim : UserClaim<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserLogin : UserLogin<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
        where TUserRole : UserRole<TKey, TRole, TUser, TUserClaim, TUserLogin, TUserRole>
    {
        public TUserRole GetByUserIdAndRoleId(TKey roleId, TKey userId)
        {
            throw new NotImplementedException();
        }
        //public TUserRole GetByUserIdAndRoleId(TKey roleId, TKey userId)
        //{
        //    return DbSet.FirstOrDefault(a => a.RoleId.Equals(roleId) && a.UserId.Equals(userId));
        //}
    }
}