using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_DAL.Interfaces;

namespace API_DAL.Helpers
{
    public class ApiRepositoryProvider : IApiRepositoryProvider, IDisposable
    {
        private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly string _instanceId = Guid.NewGuid().ToString();

        private readonly ApiRepositoryFactories _repositoryFactories;
        protected Dictionary<Type, object> Repositories { get; private set; }

        public ApiRepositoryProvider(ApiRepositoryFactories repositoryFactories)
        {
            _logger.Debug("InstanceId: " + _instanceId);

            _repositoryFactories = repositoryFactories;
            Repositories = new Dictionary<Type, object>();
        }

        public IApiRepository<T> GetRepositoryForEntityType<T>() where T : class
        {
            return GetRepository<IApiRepository<T>>(_repositoryFactories.GetRepositoryFactoryForEntityType<T>());
        }

        public virtual T GetRepository<T>(object factory = null) where T : class
        {
            // Look for T dictionary cache under typeof(T).
            object repoObj;
            Repositories.TryGetValue(typeof (T), out repoObj);
            if (repoObj != null)
            {
                return (T) repoObj;
            }

            // Not found or null; make one, add to dictionary cache, and return it.
            return MakeRepository<T>(factory);
        }

        public void SetRepository<T>(T repository)
        {
        }

        protected virtual T MakeRepository<T>(object factory)
        {
            var f = factory ?? _repositoryFactories.GetRepositoryFactory<T>();
            if (f == null)
            {
                throw new NotImplementedException("No factory for repository type, " + typeof (T).FullName);
            }
            var repo = (T) f;
            Repositories[typeof (T)] = repo;
            return repo;
        }

        public void Dispose()
        {
            _logger.Debug("InstanceId: " + _instanceId);
        }
    }
}