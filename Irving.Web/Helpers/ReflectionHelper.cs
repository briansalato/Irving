using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Irving.Web.Helpers
{
    public static class ReflectionHelper
    {
        public static string GetClassName<T>()
        {
            return typeof(T).Name;
        }
    }
}