using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Irving.Web.Controllers;
using Moq;
using Irving.Web.Models;
using System.Web.Mvc;
using Irving.Web.DAL;
using System.ComponentModel.DataAnnotations;
using Irving.Web.Helpers;

namespace Irving.Web.Tests.Controllers
{
    [TestClass]
    public class CRUDControllerTest
    {
        [TestInitialize]
        public void Init()
        {
            _mockDBRepo = new Mock<IRepository<FakeDbModel>>();
            _crudController = new FakeCRUDController(_mockDBRepo.Object);
            _crudController = TestHelper.SetupController(_crudController);
        }

        #region Create HttpGet
        [TestMethod]
        public void Create_HttpGet_Calls_View_Correctly()
        {
            //arrange

            //act
            var viewResult = _crudController.Create() as ViewResult;

            //assert
            Assert.IsNotNull(viewResult, "A ViewResult should have been returned");
            Assert.IsNull(viewResult.View, "The default view should have been used");

            var model = viewResult.Model as FakeDbModel;
            Assert.IsNotNull(model, "A non null model of type FakeDbModel should have been passed to the view");
            Assert.AreEqual(model.Id, 0, "The model passed in should have been a new one and have an Id of 0");
        }
        #endregion

        #region Create HttpPost
        [TestMethod]
        public void Create_HttpPost_When_Model_Isnt_Valid()
        {
            //arrange
            var inputModel = new FakeDbModel();
            _crudController.ModelState.AddModelError("Test", "Test Error");

            //act
            var result = _crudController.Create(inputModel) as ViewResult;

            //assert
            Assert.IsNotNull(result, "A ViewResult should have been returned");
            Assert.IsNull(result.View, "The default view should have been set");

            var resultModel = result.Model as FakeDbModel;
            Assert.AreEqual(resultModel, inputModel, "The model should have been the same one that was passed i");
        }
        
        [TestMethod]
        public void Create_HttpPost_When_Model_Is_Valid()
        {
            //arrange
            var inputModel = new FakeDbModel();

            //act
            var result = _crudController.Create(inputModel) as RedirectResult;

            //assert
            Assert.IsNotNull(result, "A RedirectResult should have been returned");
            Assert.AreEqual(result.Url, _crudController.Url.Show<FakeDbModel>(inputModel), "It should have redirected to show the new item");

            _mockDBRepo.Verify(m => m.Add(inputModel), Times.Once(), "The repo's add method should have been called");
            _mockDBRepo.Verify(m => m.SaveChanges(), Times.Once(), "The repo's save method should have been called");
                                                                      
        }
        #endregion

        #region Edit HttpGet
        [TestMethod]
        public void Edit_HttpGet_Item_Is_Not_Found()
        {
            //arrange
            var id = 5;

            _mockDBRepo.Setup(m => m.GetById(id))
                       .Returns((FakeDbModel)null);

            //act
            var actionResult = _crudController.Edit(id) as RedirectResult;

            //assert
            Assert.IsNotNull(actionResult, "The result should have been a redirect");
            Assert.AreEqual(actionResult.Url, _crudController.Url.List<FakeDbModel>(), "The url redirected to should have been the list");
        }

        [TestMethod]
        public void Edit_HttpGet_Item_Is_Found()
        {
            //arrange
            var id = 5;
            var item = new FakeDbModel() { Id = id };

            _mockDBRepo.Setup(m => m.GetById(id))
                       .Returns(item);

            //act
            var actionResult = _crudController.Edit(id) as ViewResult;

            //assert
            Assert.IsNotNull(actionResult, "A ViewResult should have been returned");
            Assert.IsNull(actionResult.View, "The default view should have been used");

            var resultModel = actionResult.Model as FakeDbModel;
            Assert.AreEqual(resultModel, item, "The model passed to the view should have been the one from the repo");
        }
        #endregion

        #region Edit Post
        [TestMethod]
        public void Edit_Post_When_Model_Isnt_Valid()
        {
            //arrange
            var inputModel = new FakeDbModel() { Id = 5 };
            _crudController.ModelState.AddModelError("Test", "Test Error");

            //act
            var result = _crudController.Edit(inputModel) as ViewResult;

            //assert
            Assert.IsNotNull(result, "A ViewResult should have been returned");
            Assert.IsNull(result.View, "The default view should have been set");

            var resultModel = result.Model as FakeDbModel;
            Assert.AreEqual(resultModel, inputModel, "The model should have been the same one that was passed in");
        }

        [TestMethod]
        public void Edit_Post_When_Model_Is_Valid()
        {
            //arrange
            var inputModel = new FakeDbModel() { Id = 5 };

            //act
            var result = _crudController.Edit(inputModel) as RedirectResult;

            //assert
            Assert.IsNotNull(result, "A RedirectResult should have been returned");
            Assert.AreEqual(result.Url, _crudController.Url.Show<FakeDbModel>(inputModel), "It should have redirected to show the item");

            _mockDBRepo.Verify(m => m.Update(inputModel), Times.Once(), "The repo's update method should have been called");
            _mockDBRepo.Verify(m => m.SaveChanges(), Times.Once(), "The repo's save method should have been called");

        }
        #endregion

        #region Show
        [TestMethod]
        public void Show_Item_Is_Not_Found()
        {
            //arrange
            var id = 5;

            _mockDBRepo.Setup(m => m.GetById(id))
                       .Returns((FakeDbModel)null);

            //act
            var actionResult = _crudController.Show(id) as RedirectResult;

            //assert
            Assert.IsNotNull(actionResult, "The result should have been a redirect");
            Assert.AreEqual(actionResult.Url, _crudController.Url.List<FakeDbModel>(), "The url redirected to should have been the list");
        }

        [TestMethod]
        public void Showt_Item_Is_Found()
        {
            //arrange
            var id = 5;
            var item = new FakeDbModel() { Id = id };

            _mockDBRepo.Setup(m => m.GetById(id))
                       .Returns(item);

            //act
            var actionResult = _crudController.Show(id) as ViewResult;

            //assert
            Assert.IsNotNull(actionResult, "A ViewResult should have been returned");
            Assert.IsNull(actionResult.View, "The default view should have been used");

            var resultModel = actionResult.Model as FakeDbModel;
            Assert.AreEqual(resultModel, item, "The model passed to the view should have been the one from the repo");
        }
        #endregion

        #region Delete
        [TestMethod]
        public void Delete_When_Delete_Fails()
        {
            //arrange
            var id = 5;

            _mockDBRepo.Setup(m => m.Delete(id))
                       .Returns(false);

            //act
            var actionResult = _crudController.Delete(id) as RedirectResult;

            //assert
            Assert.IsNotNull(actionResult, "The result should have been a redirect");
            Assert.AreEqual(actionResult.Url, _crudController.Url.Show<FakeDbModel>(id), "The url redirected to should have been to show the item");
        }

        [TestMethod]
        public void Delete_When_Delete_Is_Successful()
        {
            //arrange
            var id = 5;

            _mockDBRepo.Setup(m => m.Delete(id))
                       .Returns(true);

            //act
            var actionResult = _crudController.Delete(id) as RedirectResult;

            //assert
            _mockDBRepo.Verify(m => m.SaveChanges(), Times.Once(), "The changes need to be saved");
            Assert.IsNotNull(actionResult, "The result should have been a redirect");
            Assert.AreEqual(actionResult.Url, _crudController.Url.List<FakeDbModel>(), "The url redirected to should have been to the list");
        }
        #endregion

        #region List
        [TestMethod]
        public void List_Calls_View_Correctly()
        {
            //arrange
            var itemsFromDb = new List<FakeDbModel>();
            _mockDBRepo.Setup(m => m.Get(It.Is<Filter.DbFilter>(filter => !filter.Id.HasValue)))
                       .Returns(itemsFromDb);

            //act
            var viewResult = _crudController.List() as ViewResult;

            //assert
            Assert.IsNotNull(viewResult, "A ViewResult should have been returned");
            Assert.IsNull(viewResult.View, "The default view should have been used");

            var model = viewResult.Model as List<FakeDbModel>;
            Assert.AreEqual(model, itemsFromDb, "The model passed in should have been the list from the database");
        }
        #endregion

        private Mock<IRepository<FakeDbModel>> _mockDBRepo;
        private FakeCRUDController _crudController;

    }
    
    public class FakeCRUDController : CRUDController<FakeDbModel>
    {
        public FakeCRUDController(IRepository<FakeDbModel> dbRepo) : base(dbRepo) { }
    }

    public class FakeDbModel : DbModel { }
}
