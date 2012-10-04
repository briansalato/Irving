﻿using System.Data.Entity;
using System;
using System.Data.Entity.Infrastructure;
using Irving.Web.Models;

namespace Irving.Web.DAL
{
    public interface IIrvingDbContext : IDisposable, IObjectContextAdapter
    {
        IDbSet<Asset> Assets { get; set; }
        IDbSet<AssetType> AssetTypes { get; set; }
        IDbSet<AssetTypeProperty> AssetTypeProperties { get; set; }

        int SaveChanges();
        IDbSet<T> Set<T>() where T : Irving.Web.Models.DbModel;
        void SetAsModified<T>(T model) where T : Irving.Web.Models.DbModel;
        //void ChangeObjectState(object entity, EntityState entityState);
        //void Detach(Irving.Models.DbModel item);
    }
}