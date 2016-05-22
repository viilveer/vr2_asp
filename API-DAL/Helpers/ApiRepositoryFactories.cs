using System;
using System.Collections.Generic;
using API_DAL.Interfaces;
using API_DAL.Repositories;

namespace API_DAL.Helpers
{
    public class ApiRepositoryFactories : IDisposable
    {
        private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly string _instanceId = Guid.NewGuid().ToString();

        private readonly IDictionary<Type, object> _repositoryFactories;

        public ApiRepositoryFactories()
        {
            _logger.Debug("InstanceId: " + _instanceId);

            _repositoryFactories = GetCustomFactories();
        }

        //this ctor is for testing only, you can give here an arbitrary list of repos
        public ApiRepositoryFactories(IDictionary<Type, object> factories)
        {
            _logger.Debug("InstanceId: " + _instanceId);

            _repositoryFactories = factories;
        }

        //special repos with custom interfaces are registered here
        private static IDictionary<Type, object> GetCustomFactories()
        {
            return new Dictionary<Type, object>
            {
                {typeof (IUserIntRepository), new UserIntRepository()},
                {typeof (IUserRoleIntRepository), new UserRoleIntRepository()},
                {typeof (IUserClaimIntRepository), new UserClaimIntRepository()},
                {typeof (IUserLoginIntRepository), new UserLoginIntRepository()},
                {typeof (IRoleIntRepository), new RoleIntRepository()},
                {typeof (IVehicleRepository), new VehicleRepository()},
                {typeof (IBlogRepository), new BlogRepository()},
                {typeof (IBlogPostRepository), new BlogPostRepository()},
                {typeof (IMessageThreadRepository), new MessageThreadRepository()},
                {typeof (IMessageRepository), new MessageRepository()},
                {typeof (IMessageReceiverRepository), new MessageReceiverRepository()},
                {typeof (IMessageThreadReceiverRepository), new MessageThreadReceiverRepository()},
                {typeof (IUserBlogConnectionRepository), new UserBlogConnectionRepository()},
            };
        }

        public object GetRepositoryFactory<T>()
        {
            object factory;
            _repositoryFactories.TryGetValue(typeof (T), out factory);
            return factory;
        }

        public object GetRepositoryFactoryForEntityType<T>() where T : class
        {
            // if we already have this repository in list, return it
            // if not, create new instance of EFRepository
            return GetRepositoryFactory<T>() ?? DefaultEntityRepositoryFactory<T>();
        }

        protected virtual object DefaultEntityRepositoryFactory<T>() where T : class
        {
            // create new instance of EFRepository<T>
            return new ApiRepository<T>();
        }

        public void Dispose()
        {
            _logger.Debug("InstanceId: " + _instanceId);
        }
    }
}