using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Irving.Web.Models;
using Irving.Web.Filter;
using System.Diagnostics.CodeAnalysis;
using Irving.Web.DAL;
using Irving.Web.Helpers;

namespace Irving.Web.Controllers
{
    public abstract class CRUDController<T> : Controller where T : DbModel, new()
    {
        [HttpGet]
        public virtual ActionResult Create()
        {
            var model = new T();
            return View(model);
        }

        [HttpPost]
        public virtual RedirectResult Create(T model)
        {
            if (!ModelState.IsValid)
            {
                return Redirect(Url.Create<T>());
            }
            _modelRepo.Add(model);
            _modelRepo.SaveChanges();
            return Redirect(Url.Show(model));
        }

        [HttpGet]
        public virtual ActionResult Edit(int id)
        {
            var filter = new DbFilter() { Id = id };
            var model = _modelRepo.Get(filter).FirstOrDefault();
            if (model == null)
            {
                this.AddFlashError(Keys.NotFound<T>(), Messages.NotFound<T>());
                return Redirect(Url.List<T>());
            }

            return View(model);
        }

        [HttpPost]
        public virtual RedirectResult Edit(T model)
        {
            if (!ModelState.IsValid)
            {
                return Redirect(Url.Edit(model));
            }
            _modelRepo.Update(model);
            _modelRepo.SaveChanges();
            var result = _modelRepo.Get(new DbFilter() { Id = model.Id });
            return Redirect(Url.Show(model));
        }

        [HttpGet]
        public virtual ActionResult Show(int id)
        {
            var filter = new DbFilter() { Id = id };
            var model = _modelRepo.Get(filter).FirstOrDefault();
            if (model == null)
            {
                this.AddFlashError(Keys.NotFound<T>(), Messages.NotFound<T>());
                return Redirect(Url.Dashboard());
            }

            return View(model);
        }

        [HttpPost]
        public virtual RedirectResult Delete(int id)
        {
            var result = _modelRepo.Delete(id);
            if (!result)
            {
                this.AddFlashError(Keys.NotFound<T>(), Messages.NotFound<T>());
                return Redirect(Url.Show<T>(id));
            }

            _modelRepo.SaveChanges();
            return Redirect(Url.List<T>());
        }

        [HttpGet]
        public virtual ActionResult List()
        {
            var models = _modelRepo.Get(new DbFilter());
            return View(models);
        }
        

        #region Constructors
        public CRUDController(IRepository<T> modelRepo) 
        {
            _modelRepo = modelRepo;
        }
        #endregion

        #region Variables
        private IRepository<T> _modelRepo { get; set; }
        #endregion
    }
}
