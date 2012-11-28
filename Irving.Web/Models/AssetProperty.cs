using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Irving.Web.Models
{
    [ExcludeFromCodeCoverage]
    public class AssetProperty : DbModel
    {
        public AssetTypeProperty BaseProperty { get; set; }

        public string DbValue { get; protected set; }

        public object Value
        {
            get
            {
                return DbValue;
            }
            set
            {
                DbValue = value.ToString();
            }
        }
    }
}