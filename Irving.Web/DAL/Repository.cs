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
        public T GetById(int id)
        {
            return Get(new DbFilter<T>() { Id = id }).FirstOrDefault();
        }

        public IList<T> Get(IDbFilter<T> filter)
        {
            var query = GetIncludes();
            if (filter != null)
            {
                query = filter.Filter(query);
            }
            return query.ToList();
        }

        public IList<T> GetAll()
        {
            return Get(null);
        }

        public void Update(T itemToUpdate)
        {
            UpdateChildren(itemToUpdate);
            _db.AttachOrUpdate(itemToUpdate);
            _db.SetAsModified(itemToUpdate);
        }

        public void Add(T itemToCreate)
        {
            AddChildren(itemToCreate);
            _dbSet.Add(itemToCreate);
        }

        public void Detach(T item)
        {
            _db.Detach(item);
        }

        public bool Delete(int id)
        {
            var item = GetById(id);
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

        protected virtual void AddChildren(T itemToAdd)
        {
            //this is just for overriding, and does nothing in the base
        }

        protected virtual void UpdateChildren(T itemToModify)
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