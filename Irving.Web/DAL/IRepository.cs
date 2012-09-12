using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Irving.Web.Models;
using Irving.Web.Filter;

namespace Irving.Web.DAL
{
    public interface IRepository<T> where T : DbModel
    {
        IList<T> Get(DbFilter filter);
    }
}