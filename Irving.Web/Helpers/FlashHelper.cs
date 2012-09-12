using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Web.Mvc;

namespace Irving.Web.Helpers
{
    [ExcludeFromCodeCoverage]
    internal static partial class FlashHelper
    {
        #region -Warnings
        internal static void AddFlashWarning(this ControllerBase controller, string key, string message)
        {
            AddFlash(controller, "warnings", key, message);
        }

        internal static IDictionary<string, MvcHtmlString> GetFlashWarnings(this HtmlHelper helper)
        {
            return GetFlashMessages(helper, "warnings");
        }

        internal static IDictionary<string, MvcHtmlString> GetFlashWarnings(this ControllerBase controller)
        {
            return GetFlashMessages(controller, "warnings");
        }
        #endregion

        #region -Errors
        internal static void AddFlashError(this ControllerBase controller, string key, string message)
        {
            AddFlash(controller, "errors", key, message);
        }

        internal static IDictionary<string, MvcHtmlString> GetFlashErrors(this HtmlHelper helper)
        {
            return GetFlashMessages(helper, "errors");
        }

        internal static IDictionary<string, MvcHtmlString> GetFlashErrors(this ControllerBase controller)
        {
            return GetFlashMessages(controller, "errors");
        }
        #endregion

        #region -Successes
        internal static void AddFlashSuccess(this ControllerBase controller, string key, string message)
        {
            AddFlash(controller, "success", key, message);
        }

        internal static IDictionary<string, MvcHtmlString> GetFlashSuccesses(this HtmlHelper helper)
        {
            return GetFlashMessages(helper, "success");
        }

        internal static IDictionary<string, MvcHtmlString> GetFlashSuccesses(this ControllerBase controller)
        {
            return GetFlashMessages(controller, "success");
        }
        #endregion

        #region -private
        private static void AddFlash(ControllerBase controller, string sessionKey, string key, string message)
        {
            sessionKey = "Flash." + sessionKey;
            IDictionary<string, MvcHtmlString> messages = controller.TempData[sessionKey] as Dictionary<string, MvcHtmlString>;
            if (messages == null)
            {
                messages = new Dictionary<string, MvcHtmlString>();
            }
            if (messages.ContainsKey(key))
            {
                messages.Remove(key);
            }
            messages.Add(key, new MvcHtmlString(message.Replace("\n", "<br />")));
            controller.TempData[sessionKey] = messages;
        }

        private static IDictionary<string, MvcHtmlString> GetFlashMessages(HtmlHelper helper, string sessionKey)
        {
            sessionKey = "Flash." + sessionKey;
            return (helper.ViewContext.TempData[sessionKey] != null
                        ? (IDictionary<string, MvcHtmlString>)helper.ViewContext.TempData[sessionKey]
                        : new Dictionary<string, MvcHtmlString>());
        }

        private static IDictionary<string, MvcHtmlString> GetFlashMessages(ControllerBase controller, string sessionKey)
        {
            sessionKey = "Flash." + sessionKey;
            return (controller.TempData[sessionKey] != null
                        ? (IDictionary<string, MvcHtmlString>)controller.TempData[sessionKey]
                        : new Dictionary<string, MvcHtmlString>());
        }
        #endregion
    }
}