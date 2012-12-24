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
        public IList<DashboardAssetViewModel> Assets { get; set; }
    }

    public class DashboardAssetViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}