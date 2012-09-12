using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Irving.Web.Models;
using Irving.Web.ViewModels;
using Irving.Web.DAL;
using Irving.Web.Filter;

namespace Irving.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your quintessential app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your quintessential contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Dashboard()
        {
            var assetFilter = new AssetFilter()
            {
                User = CurrentUser
            };
            var viewModel = new DashboardViewModel()
            {
                Assets = _assetRepo.Get(assetFilter)
            };

            return View(viewModel);
        }

        #region Constructors
        public HomeController()
            : this (new AssetRepository())
        {
        }

        public HomeController(IRepository<Asset> assetRepo) 
        {
            _assetRepo = assetRepo;
        }
        #endregion

        #region Variables
        private IRepository<Asset> _assetRepo { get; set; }
        #endregion
    }
}
