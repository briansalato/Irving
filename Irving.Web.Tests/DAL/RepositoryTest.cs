using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Irving.Web.Filter;
using Moq;
using Irving.Web.Models;
using Irving.Web.DAL;

namespace Irving.Web.Tests.DAL
{
    [TestClass]
    public class RepositoryTest
    {
        [TestInitialize]
        public void Init()
        {
            _dbSet = new FakeDbSet<ConcreateBaseModel>();
            _mockDb = new Mock<IIrvingDbContext>();
            _mockDb.Setup(m => m.Set<ConcreateBaseModel>())
                   .Returns(_dbSet);
            _repo = new ConcreateRepo(_mockDb.Object);
        }

        #region Get
        [TestMethod]
        public void Get_When_Filter_Is_Null()
        {
            //arrange
            IDbFilter<ConcreateBaseModel> filter = null;
            var fakeBaseModels = new List<ConcreateBaseModel>()
                           {
                               new ConcreateBaseModel() { Id = 1 },
                               new ConcreateBaseModel() { Id = 2 },
                           };
            _dbSet.SetData(fakeBaseModels);

            //act
            var results = _repo.Get(filter);

            //assert
            TestHelper.EnumerableEqual(fakeBaseModels, results, "All items should have been returned");
        }

        [TestMethod]
        public void Get_When_Filter_Is_Not_Null()
        {
            //arrange
            var fakeBaseModels = new List<ConcreateBaseModel>()
                           {
                               new ConcreateBaseModel() { Id = 1 },
                               new ConcreateBaseModel() { Id = 2 },
                           };
            var mockFilter = new Mock<IDbFilter<ConcreateBaseModel>>();
            mockFilter.Setup(m => m.Filter(It.IsAny<IQueryable<ConcreateBaseModel>>()))
                .Returns((IQueryable<ConcreateBaseModel> query) => query.Where(item => item.Id == 2));

            _dbSet.SetData(fakeBaseModels);

            //act
            var results = _repo.Get(mockFilter.Object);

            //assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(2, results.First().Id);
        }
        #endregion

        #region GetById
        [TestMethod]
        public void GetById_When_Item_Found()
        {
            //arrange
            var id = 5;
            var fakeBaseModels = new List<ConcreateBaseModel>()
                           {
                               new ConcreateBaseModel() { Id = 1 },
                               new ConcreateBaseModel() { Id = 5 },
                               new ConcreateBaseModel() { Id = 6 },
                           };
            _dbSet.SetData(fakeBaseModels);

            //act
            var result = _repo.GetById(id);

            //assert
            Assert.AreEqual(5, result.Id, "The returned result should have been Id=5");
        }

        [TestMethod]
        public void GetById_When_No_Item_Found()
        {
            //arrange
            var id = 5;
            var fakeBaseModels = new List<ConcreateBaseModel>()
                           {
                               new ConcreateBaseModel() { Id = 1 },
                               new ConcreateBaseModel() { Id = 6 },
                           };
            _dbSet.SetData(fakeBaseModels);

            //act
            var result = _repo.GetById(id);

            //assert
            Assert.IsNull(result, "The result should have been null since there was no item");
        }
        #endregion

        #region Delete
        [TestMethod]
        public void Delete_When_Item_Exists()
        {
            //arrange
            var id = 5;
            var fakeBaseModels = new List<ConcreateBaseModel>()
                           {
                               new ConcreateBaseModel() { Id = 1 },
                               new ConcreateBaseModel() { Id = 5 },
                               new ConcreateBaseModel() { Id = 6 },
                           };
            _dbSet.SetData(fakeBaseModels);
            
            //act
            var result = _repo.Delete(id);

            //assert
            Assert.IsFalse(fakeBaseModels.Any(item => item.Id == id), "The item was not removed from the set of data");
            Assert.AreEqual(2, fakeBaseModels.Count, "More than one item was removed from the set of data");
            Assert.IsTrue(result, "The result should be true when it is deleted");
        }

        [TestMethod]
        public void Delete_When_Item_Doesnt_Exist()
        {
            //arrange
            var id = 5;
            var fakeBaseModels = new List<ConcreateBaseModel>()
                           {
                               new ConcreateBaseModel() { Id = 1 },
                               new ConcreateBaseModel() { Id = 6 }
                           };
            _dbSet.SetData(fakeBaseModels);

            //act
            var result = _repo.Delete(id);

            //assert
            Assert.AreEqual(2, fakeBaseModels.Count, "No item should have been removed from the set of data since none were found");
            Assert.IsFalse(result, "The result should be false when no item is deleted");
        }
        #endregion

        #region Add
        [TestMethod]
        public void Add_As_Success()
        {
            //arrange
            var itemToCreate = new ConcreateBaseModel();

            var fakeBaseModels = new List<ConcreateBaseModel>();
            _dbSet.SetData(fakeBaseModels);

            //act
            _repo.Add(itemToCreate);

            //assert
            Assert.AreNotEqual(0, itemToCreate.Id, "The item should have an Id");
            Assert.AreEqual(1, fakeBaseModels.Count, "The item should have been added to the list");
        }
        #endregion

        #region Update
        [TestMethod]
        public void Update_As_Success()
        {
            //arrange
            var itemToUpdate = new ConcreateBaseModel() { Id = 5 };

            //act
            _repo.Update(itemToUpdate);

            //assert
            _mockDb.Verify(m => m.AttachOrUpdate(itemToUpdate), Times.Once(), "The item needs to be attached to the context");
            _mockDb.Verify(m => m.SetAsModified(itemToUpdate), Times.Once(), "The item needs to be set to modified so it will save");
        }
        #endregion

        #region SaveChanges
        [TestMethod]
        public void SaveChanges_Calls_DB_Save()
        {
            //arrange

            //act
            _repo.SaveChanges();

            //assert
            _mockDb.Verify(m => m.SaveChanges(), Times.Once(), "The database's save method was not called");
        }
        #endregion

        #region Variables
        private FakeDbSet<ConcreateBaseModel> _dbSet;
        private Mock<IIrvingDbContext> _mockDb;
        private ConcreateRepo _repo;
        #endregion
    }

    public class ConcreateBaseModel : DbModel
    {
        //public bool WasChanged { get; set; }
    }

    class ConcreateRepo : Repository<ConcreateBaseModel> 
    {
        public ConcreateRepo(IIrvingDbContext dbContext) : base(dbContext) { }
    }
}
