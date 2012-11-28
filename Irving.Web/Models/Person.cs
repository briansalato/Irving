using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Irving.Web.Models
{
    public class Person : DbModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}