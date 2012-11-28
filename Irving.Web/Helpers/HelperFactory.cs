using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Irving.Web.Helpers
{
    public static class HelperFactory
    {
        public static IDataHelper DataHelper { get; private set; }

        public static void SetDataHelper(IDataHelper helper)
        {
            DataHelper = helper;
        }

        static HelperFactory() {
            DataHelper = new DataHelper();
        }
    }
}
