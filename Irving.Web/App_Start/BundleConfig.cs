using System.Web;
using System.Web.Optimization;

namespace Irving.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            AddJQueryTheme(bundles);
            AddSiteJS(bundles);
            AddSiteCss(bundles);
        }

        private static void AddJQueryTheme(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/css/jqueryTheme")
                .IncludeDirectory("~/Content/jquery-smoothness", "*.css"));
        }

        private static void AddSiteJS(BundleCollection bundles)
        {
            var jsBundle = new ScriptBundle("~/bundles/js/all")
                .Include("~/Scripts/jquery-{version}.js",
                    "~/Scripts/jquery.unobtrusive-ajax.js",
                    "~/Scripts/jquery-ui-{version}.js",
                    "~/Scripts/irving-global.js",
                    "~/Scripts/irving-asset.js",
                    "~/Scripts/irving-dbCrud.js");

            bundles.Add(jsBundle);
        }

        private static void AddSiteCss(BundleCollection bundles)
        {
            var lessBundle = new Bundle("~/bundles/css/site")
                .Include("~/Content/site.less");
            lessBundle.Transforms.Add(new LessTransform());
            lessBundle.Transforms.Add(new CssMinify());

            bundles.Add(lessBundle);
        }
    }
}