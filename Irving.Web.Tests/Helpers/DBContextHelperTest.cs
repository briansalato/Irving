using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Irving.Web.Helpers;
using Irving.Web.DAL;

namespace Irving.Web.Tests.Helpers
{
    [TestClass]
    public class DBContextHelperTest
    {
        [TestMethod]
        public void GetIrvingDbContext_Returns_Concreate_Type()
        {
            //arrange

            //act
            var result = DBContextHelper.GetIrvingDbContext();

            //assert
            Assert.AreEqual(typeof(IrvingDbContext), result.GetType());
        }
    }
}
