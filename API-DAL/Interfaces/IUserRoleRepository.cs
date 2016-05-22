using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Identity;

namespace API_DAL.Interfaces
{
    public interface IUserRoleIntRepository : IUserRoleRepository<int, UserRoleInt>
    {
    }

    public interface IUserRoleRepository : IUserRoleRepository<string, UserRole>
    {
    }

    public interface IUserRoleRepository<in TKey, TUserRole> : IApiRepository<TUserRole>
        where TUserRole : class
    {
        TUserRole GetByUserIdAndRoleId(TKey roleId, TKey userId);
    }
}