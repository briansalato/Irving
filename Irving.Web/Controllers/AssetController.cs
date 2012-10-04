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
using Irving.Web.Filter;

namespace Irving.Web.Controllers
{
    public class AssetController : CRUDController<Asset>
    {
        #region Constructors
        [ExcludeFromCodeCoverage]
        public AssetController() : this (new AssetRepository()) {}

        public AssetController(IRepository<Asset> assetRepo) :base(assetRepo) { }
        #endregion
    }
}
