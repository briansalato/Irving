using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Irving.Web.Models;
using System.Web.Mvc;

namespace Irving.Web.ViewModels
{
    public class DashboardViewModel
    {
        public IList<string> Alerts { get; set; }
        public IList<Asset> Assets { get; set; }
        public IList<SelectListItem> AvailableParentAssets { get; set; }
    }
}