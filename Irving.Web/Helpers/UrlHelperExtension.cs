using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using Irving.Web.Models;

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

        #region Admin
        public static string Admin_Panel(this UrlHelper helper)
        {
            return helper.Action("Index", "Asset");
        }
        #endregion

        #region CRUD
        public static string List<T>(this UrlHelper helper)
        {
            return helper.Action("List", ReflectionHelper.GetClassName<T>());
        }

        public static string Create<T>(this UrlHelper helper)
        {
            return helper.Action("Create", ReflectionHelper.GetClassName<T>());
        }

        public static string Edit<T>(this UrlHelper helper, int id)
        {
            return helper.Action("Edit", ReflectionHelper.GetClassName<T>(), new { id = id });
        }

        public static string Edit<T>(this UrlHelper helper, T item) where T : DbModel
        {
            return helper.Action("Edit", ReflectionHelper.GetClassName<T>(), new { id = item.Id });
        }

        public static string Show<T>(this UrlHelper helper, int id)
        {
            return helper.Action("Show", ReflectionHelper.GetClassName<T>(), new { id = id });
        }

        public static string Show<T>(this UrlHelper helper, T item) where T : DbModel
        {
            return helper.Action("Show", ReflectionHelper.GetClassName<T>(), new { id = item.Id });
        }

        public static string DeletePost<T>(this UrlHelper helper)
        {
            return helper.Action("Delete", ReflectionHelper.GetClassName<T>());
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