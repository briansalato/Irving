using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Irving.Web.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Irving.Web.Models
{
    public class Asset : DbModel
    {
        [Required]
        public string Name { get; set; }
        public virtual List<Note> Notes { get; set; }
    }
}