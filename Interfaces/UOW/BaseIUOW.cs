using Domain;
using Interfaces.Repositories;

namespace Interfaces.UOW
{
    public interface BaseIUOW
    {
        //save pending changes to the data store
        void Commit();
        //void RefreshAllEntities();

        //UOW Methods, that dont fit into specific repo

        //get repository for type
        T GetRepository<T>() where T : class;


        IMultiLangStringRepository MultiLangStrings { get; }
        ITranslationRepository Translations { get; }


        // Identity, PK - string
        //IUserRepository Users { get; }
        //IUserRoleRepository UserRoles { get; }
        //IRoleRepository Roles { get; }
        //IUserClaimRepository UserClaims { get; }
        //IUserLoginRepository UserLogins { get; }

        // Identity, PK - int
        IUserIntRepository UsersInt { get; }
        IUserRoleIntRepository UserRolesInt { get; }
        IRoleIntRepository RolesInt { get; }
        IUserClaimIntRepository UserClaimsInt { get; }
        IUserLoginIntRepository UserLoginsInt { get; }
    }
}