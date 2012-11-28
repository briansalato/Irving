using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Irving.Web.Models;
using Irving.Web.DAL;
using System.Linq.Expressions;

namespace Irving.Web.Helpers
{
    public class DataHelper : IDataHelper
    {
        public void UpdateChildren<T, V>(IRepository<T> itemRepo, 
                                            IRepository<V> childRepo,
                                            T item, 
                                            Expression<Func<T, IEnumerable<V>>> childSelector) where T : DbModel
                                                                                               where V : DbModel
        {
            var itemFromDb = itemRepo.GetById(item.Id);
            
            var childSelectorFunction = childSelector.Compile();

            //we call to list so that it will make a copy. If we dont make a copy then it will delete the list on detach
            var oldChildren = childSelectorFunction(itemFromDb).ToList();
            
            //we have to detach so that when we attach on the next step it doesnt think there are duplicates
            itemRepo.Detach(itemFromDb);

            var newChildren = childSelectorFunction(item);

            foreach (var newChild in newChildren)
            {
                if (oldChildren.Any(oldChild => oldChild == newChild))
                {
                    childRepo.Update(newChild);
                }
                else
                {
                    childRepo.Add(newChild);
                }
            }

            for (int i = 0; i < oldChildren.Count; i++)
            {
                if (!newChildren.Any(newChild => newChild == oldChildren[i]))
                {
                    childRepo.Delete(oldChildren[i].Id);
                }
            }
        }
    }
}