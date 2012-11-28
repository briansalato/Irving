using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Irving.Web.DAL;
using Irving.Web.Models;
using Irving.Web.Helpers;

namespace Irving.Web.Tests.DAL
{
    [TestClass]
    public class AssetTypeRepositoryTest
    {
        [TestInitialize]
        public void Init()
        {
            _dbSet = new FakeDbSet<AssetType>();
            _mockDb = new Mock<IIrvingDbContext>();
            _mockDb.Setup(m => m.Set<AssetType>())
                   .Returns(_dbSet);
            _mockAssetTypePropertyRepo = new Mock<IRepository<AssetTypeProperty>>();
            _assetTypeRepo = new AssetTypeRepository(_mockDb.Object, _mockAssetTypePropertyRepo.Object);
        }

        #region AddChildren
        [TestMethod]
        public void Add_Calls_Add_For_All_Items_In_Properties()
        {
            //arrange
            var assetTypeProperty1 = new AssetTypeProperty();
            var assetTypeProperty2 = new AssetTypeProperty();
            var itemToAdd = new AssetType() 
            {
                Properties = new List<AssetTypeProperty>() {
                    assetTypeProperty1,
                    assetTypeProperty2
                }
            };

            //act
            _assetTypeRepo.Add(itemToAdd);

            //assert
            _mockAssetTypePropertyRepo.Verify(m => m.Add(assetTypeProperty1), Times.Once(), "Add was not called for each Properties");
            _mockAssetTypePropertyRepo.Verify(m => m.Add(assetTypeProperty2), Times.Once(), "Add was not called for each item in Properties");
        }
        #endregion

        #region AddUpdateDeleteChildren
        [TestMethod]
        public void AddUpdateDeleteChildren_Gets_The_Old_Items_And_Calls_The_Helper_Correctly()
        {
            //arrange
            var id = 5;
            var oldAssetTypeProperties = new List<AssetTypeProperty>();
            var dbItems = new List<AssetType>()
            {
                new AssetType() {
                    Id = id,
                    Properties = oldAssetTypeProperties
                }
            };
            _dbSet.SetData(dbItems);

            var newAssetTypeProperties = new List<AssetTypeProperty>();
            var itemToUpdate = new AssetType() 
            { 
                Id = id,
                Properties = newAssetTypeProperties
            };

            var mockDataHelper = new Mock<IDataHelper>();
            HelperFactory.SetDataHelper(mockDataHelper.Object);

            //act
            _assetTypeRepo.Update(itemToUpdate); 

            //assert
            mockDataHelper.Verify(m => m.DoAddUpdateDelete(_mockAssetTypePropertyRepo.Object, oldAssetTypeProperties, newAssetTypeProperties), Times.Once(), "The helper method was not called correctly");

        }
        #endregion

        #region Variables
        private FakeDbSet<AssetType> _dbSet;
        private Mock<IIrvingDbContext> _mockDb;
        private AssetTypeRepository _assetTypeRepo;
        private Mock<IRepository<AssetTypeProperty>> _mockAssetTypePropertyRepo;
        #endregion
    }
}
