using System.Web.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace Irving.Web.Attributes
{
    /// <summary>
    /// This attribute will read and write the model state to the temp data when the model state is not valid
    /// </summary>
    /// <remarks></remarks>
    [ExcludeFromCodeCoverage]
    public class CopyModelStateAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// The key that will be used for writing to the temp data
        /// </summary>
        private string _key = typeof(CopyModelStateAttribute).FullName;

        /// <summary>
        /// This will write the ModelState to the TempData on a Redirect and read the TempData into the ModelState if the page returns a View
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(System.Web.Mvc.ActionExecutedContext filterContext)
        {
            //see if there are any previous values
            ModelStateDictionary modelState = (ModelStateDictionary)filterContext.Controller.TempData[_key];
            if (modelState != null)
            {
                if (filterContext.Result is ViewResult)
                {
                    filterContext.Controller.ViewData.ModelState.Merge(modelState);
                }
                else
                {
                    filterContext.Controller.TempData.Remove(_key);
                }
            }

            //add any new values
            if (!filterContext.Controller.ViewData.ModelState.IsValid &&
                (filterContext.Result is RedirectResult || filterContext.Result is RedirectToRouteResult))
            {
                filterContext.Controller.TempData[_key] = filterContext.Controller.ViewData.ModelState;
            }

            base.OnActionExecuted(filterContext);
        }
    }
}