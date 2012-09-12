using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Irving.Web.Models;

namespace Irving.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        public User CurrentUser { get; set; }
    }
}
