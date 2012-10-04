using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Irving.Web.Models
{
    public class AssetTypeProperty : DbModel
    {
        [Required]
        public string Name { get; set; }

        public int PropertyType { get; set; }

        public AssetType Owner { get; set; }
    }
}