using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using API_DAL.Interfaces;

namespace API_DAL.Repositories
{
    // this is universal base EF repository implementation, to be included in all other repos
    // covers all basic crud methods, common for all other repos
    public class ApiRepository<T> : IApiRepository<T> where T : class
    {
        private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly string _instanceId = Guid.NewGuid().ToString();

        public List<T> All
        {
            get
            {
                throw new NotImplementedException();
            }
        }


        //Constructor, requires dbContext as dependency
        public ApiRepository()
        {
            _logger.Info("_instanceId: " + _instanceId);
        }

        //public IQueryable<T> All
        //{
        //	get { return DbSet; }
        //}

        //public List<T> All => DbSet.ToList();

        //public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        //{
        //	return includeProperties.
        //		Aggregate<Expression<Func<T, object>>, IQueryable<T>>(DbSet,
        //		  (current, includeProperty) => current.Include(includeProperty));
        //	/*
        //	// non linq version
        //	foreach (var includeProperty in includeProperties)
        //	{
        //		query = query.Include(includeProperty);
        //	}
        //	return query;
        //	*/
        //}


        //public T GetById(params object[] id)
        //{
        //    //return DbSet.Find(id);
        //}

        //public void Add(T entity)
        //{
        //    //DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
        //    //if (dbEntityEntry.State != EntityState.Detached)
        //    //{
        //    //    dbEntityEntry.State = EntityState.Added;
        //    //}
        //    //else
        //    //{
        //    //    DbSet.Add(entity);
        //    //}
        //}

        //public void Update(T entity)
        //{
        //    //DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
        //    //if (dbEntityEntry.State == EntityState.Detached)
        //    //{
        //    //    DbSet.Attach(entity);
        //    //}
        //    //dbEntityEntry.State = EntityState.Modified;
        //}

        //public void Delete(T entity)
        //{
        //    //DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
        //    //if (dbEntityEntry.State != EntityState.Deleted)
        //    //{
        //    //    dbEntityEntry.State = EntityState.Deleted;
        //    //}
        //    //else
        //    //{
        //    //    DbSet.Attach(entity);
        //    //    DbSet.Remove(entity);
        //    //}
        //}

        //public void Delete(params object[] id)
        //{
        //    //var entity = GetById(id);
        //    //if (entity == null) return;
        //    //Delete(entity);
        //}



        ////public void UpdateOrInsert(T entity)
        ////{
        ////	var entityKeys = GetKeyNames(entity);
        ////	DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
        ////	if (dbEntityEntry.Property(entityKeys[0]).CurrentValue == (object)0)
        ////	{
        ////		// insert
        ////		Add(entity);
        ////	}
        ////	else
        ////	{
        ////		// update
        ////		Update(entity);
        ////	}
        ////}

        public void Dispose()
        {
            _logger.Debug("InstanceId: " + _instanceId);
        }

        public List<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public T GetById(params object[] id)
        {
            throw new NotImplementedException();
        }

        public void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(params object[] id)
        {
            throw new NotImplementedException();
        }
    }
}