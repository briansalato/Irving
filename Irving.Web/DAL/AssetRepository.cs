using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Irving.Web.Models;
using System.Data.Entity;

namespace Irving.Web.DAL
{
    public class AssetRepository : Repository<Asset>
    {
        protected override IQueryable<Asset> GetIncludes()
        {
            return _dbSet.Include(d => d.Parent)
                .Include(d => d.Notes);
        }
    }
}