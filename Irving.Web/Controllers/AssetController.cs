using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Irving.Web.Models;
using Irving.Web.DAL;
using System.Diagnostics.CodeAnalysis;
using System.Web.Script.Serialization;
using Irving.Web.Helpers;
using Irving.Web.Filter;
using Irving.Web.ViewModels;
using Irving.Web.Attributes;

namespace Irving.Web.Controllers
{
    public class AssetController : CRUDController<Asset>
    {
        [HttpPost]
        [DontValidateChildProperties]
        public ActionResult CreateAsset(CreateAssetViewModel viewModel)
        {
            return base.Create(viewModel.Asset);
        }

        [HttpPost]
        [DontValidateChildProperties]
        public ActionResult EditAsset(EditAssetViewModel viewModel)
        {
            return base.Edit(viewModel.Asset);
        }

        [HttpPost]
        [DontValidateChildProperties]
        public PartialViewResult AddNote(Note note)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception();
            }

            _noteRepo.Add(note);
            _noteRepo.SaveChanges();
            return PartialView("_Note", note);
        }

        #region Protected Methods
        protected override object SetupShowViewModel(int id)
        {
            ShowAssetViewModel viewModel = null;
            var asset = _modelRepo.GetById(id);
            if (asset != null) 
            {
                viewModel = new ShowAssetViewModel()
                {
                    Asset = asset,
                    CanDelete = asset.Children.Count == 0
                };
            }
            return viewModel;
        }

        protected override object SetupCreateViewModel()
        {
            return new CreateAssetViewModel()
            {
                Asset = new Asset(),
                AvailableParentAssets = GetAvailableParents(null)
            };
        }

        protected override object SetupEditViewModel(int id)
        {
            EditAssetViewModel viewModel = null;
            var asset = _modelRepo.GetById(id);
            if (asset != null)
            {
                viewModel = new EditAssetViewModel()
                {
                    Asset = _modelRepo.GetById(id),
                    AvailableParentAssets = GetAvailableParents(id)
                };
            }

            return viewModel;
        }
        #endregion

        #region Private Methods
        private IEnumerable<SelectListItem> GetAvailableParents(int? id)
        {
            var availParentAssetsFilter = new AssetFilter() { NoChildren = true };
            return _modelRepo.Get(availParentAssetsFilter)
                .Where(dbAsset => dbAsset.Id != id)
                .Select(dbAsset => new SelectListItem()
                {
                    Text = dbAsset.Name,
                    Value = dbAsset.Id.ToString()
                });
        }
        #endregion

        #region Constructors
        [ExcludeFromCodeCoverage]
        public AssetController() : this (new AssetRepository(), new NoteRepository()) {}

        [ExcludeFromCodeCoverage]
        public AssetController(IRepository<Asset> assetRepo, IRepository<Note> noteRepo) :base(assetRepo) 
        { 
            _noteRepo = noteRepo;
        }
        #endregion

        #region Variables
        private readonly IRepository<Note> _noteRepo;
        #endregion
    }
}
