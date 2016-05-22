using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_DAL.Interfaces
{
    public interface IApiRepositoryProvider
    {
        IApiRepository<T> GetRepositoryForEntityType<T>() where T : class;
        T GetRepository<T>(object factory = null) where T : class;
        void SetRepository<T>(T repository);
    }
}