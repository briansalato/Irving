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
using Irving.Web.Attributes;

namespace Irving.Web.Controllers
{
    public abstract class CRUDController<T> : Controller where T : DbModel, new()
    {
        //we are using the view names here so that inherited classes can still call up and it will return the correct view even if the inherited method has a 
        //different signature
        protected const string _createViewName = "Create";
        protected const string _editViewName = "Edit";
        protected const string _showViewName = "Show";
        protected const string _listViewName = "List";

        [HttpGet]
        public virtual ActionResult Create()
        {
            var viewModel = SetupCreateViewModel();
            return View(_createViewName, viewModel);
        }

        [HttpPost]
        public virtual ActionResult Create(T modelToCreate)
        {
            if (!ModelState.IsValid)
            {
                return Create();
            }
            _modelRepo.Add(modelToCreate);
            _modelRepo.SaveChanges();
            return Redirect(Url.Show(modelToCreate));
        }

        [HttpGet]
        public virtual ActionResult Edit(int id)
        {
            var viewModel = SetupEditViewModel(id);
            
            if (viewModel == null)
            {
                this.AddFlashError(Keys.NotFound<T>(), Messages.NotFound<T>());
                return Redirect(Url.List<T>());
            }

            return View(_editViewName, viewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(T modelToUpdate)
        {
            if (!ModelState.IsValid)
            {
                return Edit(modelToUpdate.Id);
            }
            _modelRepo.Update(modelToUpdate);
            _modelRepo.SaveChanges();
            return Redirect(Url.Show(modelToUpdate));
        }

        [HttpGet]
        public virtual ActionResult Show(int id)
        {
            var model = SetupShowViewModel(id);
            if (model == null)
            {
                this.AddFlashError(Keys.NotFound<T>(), Messages.NotFound<T>());
                return Redirect(Url.List<T>());
            }

            return View(_showViewName, model);
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
            return View(_listViewName, models);
        }

        #region Protected Methods
        protected virtual object SetupShowViewModel(int id)
        {
            return _modelRepo.GetById(id);
        }

        protected virtual object SetupCreateViewModel()
        {
            return new T();
        }

        protected virtual object SetupEditViewModel(int id)
        {
            return _modelRepo.GetById(id);
        }
        #endregion

        #region Constructors
        public CRUDController(IRepository<T> modelRepo) 
        {
            _modelRepo = modelRepo;
        }
        #endregion

        #region Variables
        protected IRepository<T> _modelRepo { get; set; }
        #endregion
    }
}
