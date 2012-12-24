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
        T GetById(int id);
        IList<T> Get(IDbFilter<T> filter);
        IList<T> GetAll();
        void Update(T itemToUpdate);
        void Add(T itemToCreate);
        bool Delete(int id);
        void Detach(T item);

        int SaveChanges();
    }
};