using System.Data.Entity;
using System;
using System.Data.Entity.Infrastructure;
using Irving.Web.Models;

namespace Irving.Web.DAL
{
    public interface IIrvingDbContext : IDisposable, IObjectContextAdapter
    {
        IDbSet<Car> Cars { get; set; }
        IDbSet<Asset> Assets { get; set; }

        int SaveChanges();
        IDbSet<T> Set<T>() where T : Irving.Web.Models.DbModel;
        void SetAsModified<T>(T model) where T : Models.DbModel;
        bool IsAdded(Models.DbModel model);
        void Detach(Models.DbModel model);
        void AttachOrUpdate<T>(T model) where T : Models.DbModel;
    }
}