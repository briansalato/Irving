using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Irving.Web.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Irving.Web.Models
{
    public class Asset : DbModel
    {
        [Required]
        public string Name { get; set; }
        public List<Event> Events { get; set; }
    }
}