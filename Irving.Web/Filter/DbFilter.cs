using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Irving.Web.Models;

namespace Irving.Web.Filter
{
    public class DbFilter<T> : IDbFilter<T> where T : DbModel
    {
        public int? Id { get; set; }

        public virtual IQueryable<T> Filter(IQueryable<T> query)
        {
            if (Id.HasValue)
            {
                query = query.Where(item => item.Id == Id.Value);
            }

            return query;
        }
    }
}