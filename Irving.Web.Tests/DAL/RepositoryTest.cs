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
            DbFilter filter = null;
            var fakeBaseModels = new List<ConcreateBaseModel>()
                           {
                               new ConcreateBaseModel() { Id = 1 },
                               new ConcreateBaseModel() { Id = 2 },
                           };
            _dbSet.SetData(fakeBaseModels);

            //act
            var results = _repo.Get(filter);

            //assert
            AssertHelper.EnumerableEqual(fakeBaseModels, results, "All items should have been returned");
        }

        [TestMethod]
        public void Get_When_All_Properties_Are_Null()
        {
            //arrange
            var filter = new DbFilter();
            var fakeBaseModels = new List<ConcreateBaseModel>()
                           {
                               new ConcreateBaseModel() { Id = 1 },
                               new ConcreateBaseModel() { Id = 5 },
                               new ConcreateBaseModel() { Id = 6 },
                           };
            _dbSet.SetData(fakeBaseModels);

            //act
            var results = _repo.Get(filter);

            //assert
            AssertHelper.EnumerableEqual(fakeBaseModels, results, "All items should have been returned");
        }

        [TestMethod]
        public void Get_When_Id_Is_Not_Null()
        {
            //arrange
            var filter = new DbFilter()
            {
                Id = 5
            };
            var fakeBaseModels = new List<ConcreateBaseModel>()
                           {
                               new ConcreateBaseModel() { Id = 1 },
                               new ConcreateBaseModel() { Id = 5 },
                               new ConcreateBaseModel() { Id = 6 },
                           };
            _dbSet.SetData(fakeBaseModels);

            //act
            var result = _repo.Get(filter);

            //assert
            Assert.AreEqual(1, result.Count, "The should have been one result returned");
            Assert.AreEqual(5, result.Single().Id, "The returned result should have been Id=5");
        }
        #endregion

        #region Variables
        private FakeDbSet<ConcreateBaseModel> _dbSet;
        private Mock<IIrvingDbContext> _mockDb;
        private ConcreateRepo _repo;
        #endregion
    }

    class ConcreateBaseModel : DbModel
    {
        //public bool WasChanged { get; set; }
    }

    class ConcreateRepo : Repository<ConcreateBaseModel> 
    {
        public ConcreateRepo(IIrvingDbContext dbContext) : base(dbContext) { }
    }
}
