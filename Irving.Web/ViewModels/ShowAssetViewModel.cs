using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Irving.Web.Models;

namespace Irving.Web.ViewModels
{
    public class ShowAssetViewModel
    {
        public Asset Asset { get; set; }
        public bool CanDelete { get; set; }
    }
}