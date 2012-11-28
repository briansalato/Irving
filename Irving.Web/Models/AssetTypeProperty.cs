using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Irving.Web.Models
{
    [ExcludeFromCodeCoverage]
    public class AssetTypeProperty : DbModel
    {
        [Required]
        public string Name { get; set; }

        public int PropertyType { get; set; }

        public PropertyType PropertyTypeEnum
        {
            get
            {
                return (PropertyType)PropertyType;
            }
            set
            {
                PropertyType = (int)value;
            }
        }
    }
}