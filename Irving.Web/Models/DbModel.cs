using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Irving.Web.Models
{
    public abstract class DbModel
    {
        public int Id { get; set; }
    }
}