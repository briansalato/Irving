﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Irving.Web;
using Irving.Web.Controllers;
using Irving.Web.ViewModels;
using Irving.Web.Models;
using Moq;
using Irving.Web.DAL;
using Irving.Web.Filter;

namespace Irving.Web.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Dashboard_No_Errors()
        {
            //arrange
            var user = new User();
            var assets = new List<Asset>();
            var mockAssetRepo = new Mock<IRepository<Asset>>();
            mockAssetRepo.Setup(m => m.Get(It.Is<DbFilter<Asset>>(f => f.Id.HasValue == false)))
                       .Returns(assets);

            var controller = new HomeController(mockAssetRepo.Object);
            controller.CurrentUser = user;
            
            //act
            var result = controller.Dashboard() as ViewResult;

            //assert
            Assert.IsNotNull(result, "The result type should have been a ViewResult");

            Assert.AreEqual(null, result.View, "The default view should be used (null");
            
            var model = result.Model as DashboardViewModel;
            Assert.IsNotNull(result, "The view model type should have been a DashboardViewModel");

            Assert.AreEqual(assets, model.Assets, "The cars passed in whas not correct");
        }
    }
}
