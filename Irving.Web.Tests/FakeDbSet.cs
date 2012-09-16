using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Irving.Web.Models;
using System.Data.Entity;
using System.Collections.ObjectModel;
using System.Collections;

namespace Irving.Web.Tests
{
    internal class FakeDbSet<TEntity> : IDbSet<TEntity> where TEntity : DbModel
    {
        #region Public Methods
        public TEntity Add(TEntity entity)
        {
            int newId = _items.Count == 0 ? 1 : _items.Max(i => i.Id) + 1;
            entity.Id = newId;
            _items.Add(entity);
            return entity;
        }

        public TEntity Attach(TEntity entity)
        {
            var item = _items.Single(i => i.Id == entity.Id);
            _items[_items.IndexOf(item)] = entity;
            return entity;
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, TEntity
        {
            throw new NotImplementedException();
        }

        public TEntity Create()
        {
            throw new NotImplementedException();
        }

        public TEntity Find(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<TEntity> Local
        {
            get { throw new NotImplementedException(); }
        }

        public TEntity Remove(TEntity entity)
        {
            _items.Remove(entity);
            return entity;
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        public Type ElementType
        {
            get { throw new NotImplementedException(); }
        }

        public System.Linq.Expressions.Expression Expression
        {
            get { return _items.AsQueryable().Expression; }
        }

        public IQueryProvider Provider
        {
            get { return _items.AsQueryable().Provider; }
        }

        public void SetData(TEntity item)
        {
            SetData(new List<TEntity>() { item });
        }
        public void SetData(IList<TEntity> data)
        {
            _items = data;
        }
        #endregion

        #region Constructors
        public FakeDbSet()
        {
            SetData(new List<TEntity>());
        }
        #endregion

        #region Variables
        private IList<TEntity> _items;
        #endregion
    }
}
