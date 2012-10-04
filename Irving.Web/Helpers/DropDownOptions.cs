using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Irving.Web.Models;

namespace Irving.Web.Helpers
{
    public static class DropDownOptions
    {
        public static List<SelectListItem> PropertyTypes { get; private set; }

        static DropDownOptions() 
        {
            PropertyTypes = new List<SelectListItem>();
            var enumValues = EnumerationHelper.ToList<PropertyType>();
            foreach (var enumValue in enumValues)
            {
                PropertyTypes.Add(new SelectListItem() { Text = enumValue.GetDescription(), Value = ((int)enumValue).ToString() });
            }
        }
    }
}