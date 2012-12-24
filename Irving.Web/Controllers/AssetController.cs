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
    public class AssetController : BaseController
    {
        [HttpGet]
        public virtual ActionResult Show(int id)
        {
            var asset = _assetRepo.GetById(id);
            if (asset == null) 
            {
                this.AddFlashError(Keys.NotFound<Asset>(), Messages.NotFound<Asset>());
                return Redirect(Url.List<Asset>());
            }

            var viewModel = new ShowAssetViewModel()
            {
                Name = asset.Name,
                Id = asset.Id,
                Notes = asset.Notes.ConvertAll<NoteViewModel>(note => new NoteViewModel()
                {
                    Text = note.Text,
                    Title = note.Title,
                    Date = note.Date
                })
            };

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = new CreateAssetViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(CreateAssetViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return Create();
            }

            var newAsset = new Asset()
            {
                Name = viewModel.Name
            };

            _assetRepo.Add(newAsset);
            _assetRepo.SaveChanges();
            return Redirect(Url.Show(newAsset));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var asset = _assetRepo.GetById(id);

            if (asset == null)
            {
                this.AddFlashError(Keys.NotFound<Asset>(), Messages.NotFound<Asset>());
                return Redirect(Url.List<Asset>());
            }

            var viewModel = new EditAssetViewModel()
            {
                Id = asset.Id,
                Name = asset.Name
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(EditAssetViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return Edit(viewModel.Id);
            }

            var assetToUpdate = new Asset()
            {
                Id = viewModel.Id,
                Name = viewModel.Name
            };

            _assetRepo.Update(assetToUpdate);
            _assetRepo.SaveChanges();
            return Redirect(Url.Show(assetToUpdate));
        }

        [HttpPost]
        public virtual RedirectResult Delete(int id)
        {
            var result = _assetRepo.Delete(id);
            if (!result)
            {
                this.AddFlashError(Keys.NotFound<Asset>(), Messages.NotFound<Asset>());
                return Redirect(Url.Show<Asset>(id));
            }

            _assetRepo.SaveChanges();
            return Redirect(Url.Dashboard());
        }

        [HttpPost]
        public PartialViewResult AddNote(NoteViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception();
            }

            var note = new Note()
            {
                Text = viewModel.Text,
                Title = viewModel.Title,
                Date = viewModel.Date,
                AssetId = viewModel.AssetId
            };

            _noteRepo.Add(note);
            _noteRepo.SaveChanges();
            return PartialView("_Note", viewModel);
        }

        #region Constructors
        [ExcludeFromCodeCoverage]
        public AssetController() : this (new AssetRepository(), new NoteRepository()) {}

        [ExcludeFromCodeCoverage]
        public AssetController(IRepository<Asset> assetRepo, IRepository<Note> noteRepo)
        {
            _assetRepo = assetRepo;
            _noteRepo = noteRepo;
        }
        #endregion

        #region Variables
        private readonly IRepository<Asset> _assetRepo;
        private readonly IRepository<Note> _noteRepo;
        #endregion
    }
}
