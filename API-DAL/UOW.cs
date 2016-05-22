using System;
using API_DAL.Interfaces;
using Domain;
using NLog;

namespace API_DAL
{
    public class UOW : IUOW, IDisposable
    {
        private readonly NLog.ILogger _logger;
        private readonly string _instanceId = Guid.NewGuid().ToString();

        protected IApiRepositoryProvider RepositoryProvider { get; set; }

        public UOW(IApiRepositoryProvider repositoryProvider, ILogger logger)
        {
            _logger = logger;
            _logger.Debug("InstanceId: " + _instanceId);

           
            RepositoryProvider = repositoryProvider;
        }


        //standard repos
        public IApiRepository<MultiLangString> MultiLangStrings => GetStandardRepo<MultiLangString>();
        public IApiRepository<Translation> Translations => GetStandardRepo<Translation>();

        // repo with custom methods
        // add it also in EFRepositoryFactories.cs, in method GetCustomFactories

        //public IUserRepository Users => GetRepo<IUserRepository>();
        //public IUserRoleRepository UserRoles => GetRepo<IUserRoleRepository>();
        //public IRoleRepository Roles => GetRepo<IRoleRepository>();
        //public IUserClaimRepository UserClaims => GetRepo<IUserClaimRepository>();
        //public IUserLoginRepository UserLogins => GetRepo<IUserLoginRepository>();
        public IUserIntRepository UsersInt => GetRepo<IUserIntRepository>();
        public IUserRoleIntRepository UserRolesInt => GetRepo<IUserRoleIntRepository>();
        public IRoleIntRepository RolesInt => GetRepo<IRoleIntRepository>();
        public IUserClaimIntRepository UserClaimsInt => GetRepo<IUserClaimIntRepository>();
        public IUserLoginIntRepository UserLoginsInt => GetRepo<IUserLoginIntRepository>();

        // calling standard EF repo provider
        private IApiRepository<T> GetStandardRepo<T>() where T : class
        {
            return RepositoryProvider.GetRepositoryForEntityType<T>();
        }

        // calling custom repo provier
        private T GetRepo<T>() where T : class
        {
            return RepositoryProvider.GetRepository<T>();
        }

        // try to find repository
        public T GetRepository<T>() where T : class
        {
            var res = GetRepo<T>() ?? GetStandardRepo<T>() as T;
            if (res == null)
            {
                throw new NotImplementedException("No repository for type, " + typeof(T).FullName);
            }
            return res;
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _logger.Debug("InstanceId: " + _instanceId + " Disposing:" + disposing);
        }

        #endregion
    }
}