using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Irving.Web.Models;

namespace Irving.Web.Filter
{
    public interface IDbFilter<T> where T : DbModel
    {
        IQueryable<T> Filter(IQueryable<T> query);
    }
}