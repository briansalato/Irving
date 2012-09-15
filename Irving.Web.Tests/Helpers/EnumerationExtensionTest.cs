using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Irving.Web.Helpers;

namespace Irving.Web.Tests.Helpers
{
    [TestClass]
    public class EnumerationExtensionTest
    {
        #region GetDescription
        [TestMethod]
        public void GetDescription_When_The_Enum_Has_A_Description()
        {
            //arrange

            //act
            var result = TestEnum.First.GetDescription();

            //assert
            Assert.AreEqual("Test description", result, "The description attribute was not used");
        }

        [TestMethod]
        public void GetDescription_When_The_Enum_Doesnt_Have_A_Description()
        {
            //arrange

            //act
            var result = TestEnum.Second.GetDescription();

            //assert
            Assert.AreEqual("Second", result, "The ToString should have been used");
        }
        #endregion
    }

    enum TestEnum
    {
        [System.ComponentModel.Description("Test description")]
        First,
        Second
    }
}
