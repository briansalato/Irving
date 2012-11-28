using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Irving.Web.Models;
using System.Data.Entity;
using Irving.Web.Helpers;
using Irving.Web.Filter;
using System.Diagnostics.CodeAnalysis;

namespace Irving.Web.DAL
{
    public class AssetTypeRepository : Repository<AssetType>
    {
        protected override IQueryable<AssetType> GetIncludes()
        {
            return _dbSet.Include(m => m.Properties)
                         .AsQueryable();
        }

        protected override void AddChildren(AssetType itemToAdd)
        {
            foreach (var item in itemToAdd.Properties)
            {
                _assetTypePropertyRepo.Add(item);
            }
        }

        protected override void AddUpdateDeleteChildren(AssetType itemToModify)
        {
            var dbItem = base.GetById(itemToModify.Id);
            //we call to list so that it will make a copy. If we dont make a copy then it will delete the list on detach
            var oldItems = dbItem.Properties.ToList();
            //we have to detach so that when we attach on the next step it doesnt think there are duplicates
            _db.Detach(dbItem);
            HelperFactory.DataHelper.DoAddUpdateDelete(_assetTypePropertyRepo, oldItems, itemToModify.Properties);
        }

        #region Constructors
        [ExcludeFromCodeCoverage]
        public AssetTypeRepository()
        {
            _assetTypePropertyRepo = new AssetTypePropertyRepository();
        }

        public AssetTypeRepository(IIrvingDbContext dbContext, IRepository<AssetTypeProperty> assetTypePropertyRepo)
            :base (dbContext)
        {
            _assetTypePropertyRepo = assetTypePropertyRepo;
        }
        #endregion

        #region Variables
        private IRepository<AssetTypeProperty> _assetTypePropertyRepo { get; set; }
        #endregion
    }
}