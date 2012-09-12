using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Irving.Web.Models;
using Irving.Web.Filter;

namespace Irving.Web.DAL
{
    public abstract class Repository<T> : IRepository<T> where T : DbModel
    {
        public IList<T> Get(DbFilter filter)
        {
            throw new NotImplementedException();
        }
    }
}