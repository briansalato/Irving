using System.Data.Entity;
using System;
using System.Data.Entity.Infrastructure;
using Irving.Web.Models;

namespace Irving.Web.DAL
{
    public interface IIrvingDbContext : IDisposable, IObjectContextAdapter
    {
        IDbSet<Asset> Assets { get; set; }

        //int SaveChanges();
        //IDbSet<T> Set<T>() where T : Irving.Models.DbModel;
        //void ChangeObjectState(object entity, EntityState entityState);
        //void Detach(Irving.Models.DbModel item);
    }
}