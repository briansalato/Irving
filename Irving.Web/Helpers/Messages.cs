
namespace Irving.Web.Helpers
{
    public static class Messages
    {
        public const string ASSET_NOT_FOUND = "The asset you request was not found.";
        //public const string THING_DELETED = "The thing was successfully removed.";

        public static string NotFound<T>()
        {
            return string.Format("The {0} you requested was not found.", ReflectionHelper.GetClassName<T>().ToLower());
        }
    }
}