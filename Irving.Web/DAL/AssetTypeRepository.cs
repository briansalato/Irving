using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Irving.Web.Models;
using System.Data.Entity;

namespace Irving.Web.DAL
{
    public class AssetTypeRepository : Repository<AssetType>
    {
        protected override IQueryable<AssetType> GetIncludes()
        {
            return _dbSet.Include(m => m.Properties)
                         .AsQueryable();
        }
    }
}