using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Irving.Web.Models;

namespace Irving.Web.Filter
{
    public class AssetFilter : DbFilter
    {
        public User User { get; set; }
    }
}