using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Irving.Web.Models;

namespace Irving.Web.DAL
{
    public class IrvingDbContext :  DbContext, IIrvingDbContext, IObjectContextAdapter
    {
        public IDbSet<Asset> Assets { get; set; }
    }
}