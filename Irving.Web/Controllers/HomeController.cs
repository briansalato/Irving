using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Irving.Web.Models;
using Irving.Web.ViewModels;
using Irving.Web.DAL;
using Irving.Web.Filter;
using System.Diagnostics.CodeAnalysis;

namespace Irving.Web.Controllers
{
    public class HomeController : BaseController
    {
        [HttpGet]
        public ActionResult Dashboard()
        {
            var viewModel = new DashboardViewModel()
            {
                Assets = _assetRepo.GetAll().Select(asset => new DashboardAssetViewModel() 
                {
                    Id = asset.Id,
                    Name = asset.Name
                }).ToList()
            };

            return View(viewModel);
        }

        #region Constructors
        [ExcludeFromCodeCoverage]
        public HomeController()
        {
            _assetRepo = new AssetRepository();
        }

        public HomeController(IRepository<Asset> assetRepo)
        {
            _assetRepo = assetRepo;
        }
        #endregion

        #region Variables
        private IRepository<Asset> _assetRepo;
        #endregion
    }
}
