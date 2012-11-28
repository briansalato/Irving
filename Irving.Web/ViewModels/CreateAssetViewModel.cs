using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Irving.Web.Models;
using System.Web.Mvc;

namespace Irving.Web.ViewModels
{
    public class CreateAssetViewModel
    {
        public Asset Asset { get; set; }
        public IEnumerable<SelectListItem> AvailableParentAssets { get; set; }
    }
}