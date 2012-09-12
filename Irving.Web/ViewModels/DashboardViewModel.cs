using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Irving.Web.Models;

namespace Irving.Web.ViewModels
{
    public class DashboardViewModel
    {
        public IList<Asset> Assets { get; set; }
    }
}