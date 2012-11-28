using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Irving.Web.DAL;
using Irving.Web.Models;
using System.Diagnostics.CodeAnalysis;
using Irving.Web.Filter;

namespace Irving.Web.Controllers
{
    public class AssetTypeController : CRUDController<AssetType>
    {
        #region Constructors
        [ExcludeFromCodeCoverage]
        public AssetTypeController() : this (new AssetTypeRepository()) { }

        [ExcludeFromCodeCoverage]
        public AssetTypeController(IRepository<AssetType> assetTypeRepo) : base(assetTypeRepo) { }
        #endregion

    }
}
