using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Irving.Web.Models;
using Irving.Web.Filter;
using System.Data.Entity;
using Irving.Web.Helpers;
using System.Diagnostics.CodeAnalysis;

namespace Irving.Web.DAL
{
    public abstract class Repository<T> : IRepository<T> where T : DbModel
    {
        public IList<T> Get(DbFilter filter)
        {
            return GetFilterQuery(filter).ToList();
        }

        public void Add(T itemToCreate)
        {
            _dbSet.Add(itemToCreate);
        }

        public int SaveChanges()
        {
            return _db.SaveChanges();
        }

        #region Protected Methods
        protected virtual IQueryable<T> GetIncludes()
        {
            return _dbSet.AsQueryable();
        }

        protected virtual IQueryable<T> GetFilterQuery(DbFilter filter)
        {
            var queryable = GetIncludes();
            if (filter != null)
            {
                if (filter.Id.HasValue)
                {
                    queryable = queryable.Where(q => q.Id == filter.Id.Value);
                }
            }

            return queryable;
        }
        #endregion
        
        #region Constructors
        [ExcludeFromCodeCoverage]
        public Repository()
        {
            Init(DBContextHelper.GetIrvingDbContext());
        }

        public Repository(IIrvingDbContext dbContext)
        {
            Init(dbContext);
        }

        private void Init(IIrvingDbContext dbContext)
        {
            _db = dbContext;
            _dbSet = _db.Set<T>();
        }
        #endregion

        #region Properties
        protected IDbSet<T> _dbSet { get; private set; }
        protected IIrvingDbContext _db { get; set; }
        #endregion
    }
}