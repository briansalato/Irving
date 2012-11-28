using System;
using Irving.Web.DAL;
using System.Collections.Generic;
using Irving.Web.Models;
using System.Linq.Expressions;

namespace Irving.Web.Helpers
{
    public interface IDataHelper
    {
        void UpdateChildren<T, V>(IRepository<T> itemRepo,
                                     IRepository<V> propertyRepo,
                                     T item,
                                     Expression<Func<T, IEnumerable<V>>> property)
            where T : DbModel
            where V : DbModel;
    }
}
