using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Irving.Web.Models;
using System.Diagnostics.CodeAnalysis;

namespace Irving.Web.DAL
{
    [ExcludeFromCodeCoverage]
    public class IrvingDbContext :  DbContext, IIrvingDbContext, IObjectContextAdapter
    {
        public IDbSet<Car> Cars { get; set; }

        public IDbSet<Asset> Assets { get; set; }

        public IDbSet<T> Set<T>() where T : Models.DbModel
        {
            return base.Set<T>();
        }

        public int SaveChanges()
        {
            return base.SaveChanges();
        }

        public void SetAsModified<T>(T model) where T : Models.DbModel
        {
            base.Entry(GetCachedVersion(model) ?? model).State = System.Data.EntityState.Modified;
        }

        public bool IsAdded(Models.DbModel model)
        {
            return base.Entry(model).State == System.Data.EntityState.Added;
        }

        public void Detach(Models.DbModel model)
        {
            base.Entry(model).State = System.Data.EntityState.Detached;
        }

        public void AttachOrUpdate<T>(T model) where T : Models.DbModel
        {
            if (IsAttached(model))
            {
                UpdateCachedValues(model);
            }
            else
            {
                base.Set<T>().Attach(model);
            }
        }
        
        #region Private Methods
        private bool IsAttached<T>(T model) where T : Models.DbModel
        {
            return GetCachedVersion(model) != null;
        }

        private T GetCachedVersion<T>(T model) where T : Models.DbModel
        {
            return base.Set<T>().Local.FirstOrDefault(item => item == model);
        }

        private void UpdateCachedValues<T>(T model) where T : Models.DbModel
        {
            base.Entry(GetCachedVersion(model)).CurrentValues.SetValues(model);
        }
        #endregion
    }
}