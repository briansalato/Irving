using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;
using System.Diagnostics.CodeAnalysis;

namespace Irving.Web.Helpers
{
    [ExcludeFromCodeCoverage]
    public static partial class HtmlExtensions
    {
        public static MvcHtmlString ClientIdFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression)
        {
            return MvcHtmlString.Create(
                    htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(
                        ExpressionHelper.GetExpressionText(expression)));
        }
    }
}