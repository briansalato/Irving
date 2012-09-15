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
    public class IrvingDbContext :  DbContext, IIrvingDbContext, IObjectContextAdapter
    {
        public IDbSet<Asset> Assets { get; set; }

        [ExcludeFromCodeCoverage]
        public IDbSet<T> Set<T>() where T : Models.DbModel
        {
            return base.Set<T>();
        }

        [ExcludeFromCodeCoverage]
        public int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}