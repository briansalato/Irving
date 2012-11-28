using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Irving.Web.Attributes
{
    public class DontValidateChildProperties : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var modelState = filterContext.Controller.ViewData.ModelState;
            var valueProvider = filterContext.Controller.ValueProvider;

            var keysWithNoIncomingValue = modelState.Keys.Where(x => !valueProvider.ContainsPrefix(x));
            foreach (var key in modelState.Keys)
            {
                //a period seperates properties so if it has more than one then it is a child property
                if (key.Count(c => c == '.') > 1)
                {
                    modelState[key].Errors.Clear();
                }
            }
        }
    }
}