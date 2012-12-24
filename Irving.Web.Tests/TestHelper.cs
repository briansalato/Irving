using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web;
using Moq;

namespace Irving.Web.Tests
{
    public static class TestHelper
    {
        public static void EnumerableEqual(IEnumerable<object> expected, IEnumerable<object> actual, string message)
        {
            //if both are null then they are equal and we dont need to compare
            if (expected != null
                || actual != null)
            {
                //if only one is null then the comparision fails
                if ((expected == null && actual != null)
                    || (expected != null && actual == null))
                {
                    Assert.Fail(message);
                }

                //they should be the same size
                Assert.AreEqual(expected.Count(), actual.Count(), message);

                //each item should be the same
                var expectedList = expected.ToList();
                var actualList = actual.ToList();
                for (int i = 0; i < expectedList.Count; i++)
                {
                    Assert.AreEqual(expectedList[i], actualList[i], message);
                }
            }
        }

        public static T SetupController<T>(T controller) where T : Controller
        {
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            var request = new Mock<HttpRequestBase>(MockBehavior.Strict);
            request.SetupGet(r => r.ApplicationPath).Returns("/");
            request.SetupGet(x => x.ServerVariables).Returns(new System.Collections.Specialized.NameValueCollection());
            var reqParams = new System.Collections.Specialized.NameValueCollection();
            request.SetupGet(r => r.Params).Returns(reqParams);
            var queryString = new System.Collections.Specialized.NameValueCollection();
            request.SetupGet(x => x.QueryString).Returns(queryString);

            var response = new Mock<HttpResponseBase>(MockBehavior.Strict);
            response.Setup(x => x.ApplyAppPathModifier(It.IsAny<string>())).Returns((string s) => "http://localhost/" + s);

            var context = new Mock<HttpContextBase>(MockBehavior.Strict);
            context.SetupGet(x => x.Request).Returns(request.Object);
            context.SetupGet(x => x.Response).Returns(response.Object);
            //var userMock = new Mock<IPrincipal>();
            //var identity = new Mock<IIdentity>();
            //identity.SetupGet(i => i.Name).Returns(_userName);
            //userMock.SetupGet(p => p.Identity).Returns(identity.Object);
            //context.SetupGet(x => x.User).Returns(userMock.Object);
            
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            controller.Url = new UrlHelper(new RequestContext(context.Object, new RouteData()), routes);
            return controller;
        }
    }
}
