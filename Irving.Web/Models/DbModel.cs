using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Irving.Web.Models
{
    public abstract class DbModel
    {
        [Key]
        public int Id { get; set; }
    }
}