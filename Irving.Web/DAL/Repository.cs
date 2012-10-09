using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Irving.Web.Models;
using Irving.Web.Filter;
using System.Data.Entity;
using Irving.Web.Helpers;
using System.Diagnostics.CodeAnalysis;
using System.Data;

namespace Irving.Web.DAL
{
    public abstract class Repository<T> : IRepository<T> where T : DbModel
    {
        public IList<T> Get(DbFilter filter)
        {
            return GetFilterQuery(filter).ToList();
        }

        public void Update(T itemToUpdate)
        {
            _dbSet.Attach(itemToUpdate);
            AddOrUpdateChildren(itemToUpdate);
            _db.SetAsModified(itemToUpdate);
        }

        public void Add(T itemToCreate)
        {
            _dbSet.Add(itemToCreate);
            AddOrUpdateChildren(itemToCreate);
        }

        public bool Delete(int id)
        {
            var item = _dbSet.FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                return false;
            }

            _dbSet.Remove(item);
            return true;
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
            var queryable = this.GetIncludes();
            if (filter != null)
            {
                if (filter.Id.HasValue)
                {
                    queryable = queryable.Where(q => q.Id == filter.Id.Value);
                }
            }

            return queryable;
        }

        protected virtual void AddOrUpdateChildren(T itemToModify)
        {
            //this is just for overriding, and does nothing in the base
        }
        #endregion
        
        #region Constructors
        [ExcludeFromCodeCoverage]
        public Repository()
        {
            Init(DBContextHelper.GetDataContext("Irving"));
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