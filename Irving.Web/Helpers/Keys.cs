using Irving.Web.Models;

namespace Irving.Web.Helpers
{
    public static class Keys
    {
        //public const string THING_DELETED = "ThingDeleted";

        public static string NotFound<T>()
        {
            return ReflectionHelper.GetClassName<T>() + "NotFound";
        }
    }
}