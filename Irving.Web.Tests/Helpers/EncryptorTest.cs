using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Irving.Web.Helpers;

namespace Irving.Web.Tests.Helpers
{
    [TestClass]
    public class EncryptorTest
    {
        [TestMethod]
        public void Encrypt_Does_Nothing()
        {
            //arrange
            var toEncrypt = "PreMessage";

            //act
            var result = Encryptor.Encrypt(toEncrypt);

            //assert
            Assert.AreEqual(toEncrypt, result);
        }
    }
}
