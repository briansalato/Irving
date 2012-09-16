﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Irving.Web.Models;
using Irving.Web.DAL;
using System.Diagnostics.CodeAnalysis;
using System.Web.Script.Serialization;
using Irving.Web.Helpers;

namespace Irving.Web.Controllers
{
    public class AssetController : Controller
    {
        [HttpGet]
        public ActionResult Create()
        {
            var asset = new Asset();
            return View(asset);
        }

        [HttpPost]
        public RedirectResult Create(Asset asset)
        {
            _assetRepo.Add(asset);
            _assetRepo.SaveChanges();
            return Redirect(Url.Dashboard());
        }

        #region Constructors
        [ExcludeFromCodeCoverage]
        public AssetController()
            : this (new AssetRepository())
        {
        }

        public AssetController(IRepository<Asset> assetRepo) 
        {
            _assetRepo = assetRepo;
        }
        #endregion

        #region Variables
        private IRepository<Asset> _assetRepo { get; set; }
        #endregion
    }
}