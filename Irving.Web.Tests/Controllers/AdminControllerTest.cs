using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Irving.Web.Controllers;
using System.Web.Mvc;

namespace Irving.Web.Tests.Controllers
{
    [TestClass]
    public class AdminControllerTest
    {
        #region Index
        [TestMethod]
        public void Index_Calls_View_Correctly()
        {
            //arrange
            var controller = new AdminController();

            //act
            var viewResult = controller.Index() as ViewResult;

            //assert
            Assert.IsNotNull(viewResult, "A ViewResult should have been returned");
            Assert.IsNull(viewResult.View, "The default view should have been used");
        }
        #endregion
    }
}
