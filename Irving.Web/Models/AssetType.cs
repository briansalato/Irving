﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Irving.Web.Models
{
    [ExcludeFromCodeCoverage]
    public class AssetType : DbModel
    {
        [Required]
        public string Name { get; set; }

        public List<AssetTypeProperty> Properties { get; set; }

        public AssetType()
        {
            Properties = new List<AssetTypeProperty>();
        }
    }
}