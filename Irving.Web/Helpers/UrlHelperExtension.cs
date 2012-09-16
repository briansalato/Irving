using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;

namespace Irving.Web.Helpers
{
    [ExcludeFromCodeCoverage]
    public static class UrlHelperExtension
    {
        public static string Dashboard(this UrlHelper helper)
        {
            return helper.Action("Dashboard", "Home");
        }

        #region Account
        public static string Account_Register(this UrlHelper helper)
        {
            return helper.Action("Register", "Account");
        }

        public static string Login(this UrlHelper helper)
        {
            return helper.Action("Login", "Account");
        }
        #endregion

        #region Asset
        public static string Asset_Create(this UrlHelper helper)
        {
            return helper.Action("Create", "Asset");
        }
        #endregion

        public static string Home(this UrlHelper helper)
        {
            return helper.Action("Index", "Home");
        }

        #region -Sitewide
        public static string Script(this UrlHelper helper, string fileName)
        {
            return helper.Content("~/scripts/" + fileName);
        }

        public static string Stylesheet(this UrlHelper helper, string fileName)
        {
            return helper.Content("~/content/" + fileName);
        }

        public static string ThirdPartyContent(this UrlHelper helper, string fileName)
        {
            return helper.Content("~/content/thirdParty/" + fileName);
        }

        public static string Image(this UrlHelper helper, string imageName)
        {
            return helper.Content("~/content/images/" + imageName);
        }
        #endregion
    }
}