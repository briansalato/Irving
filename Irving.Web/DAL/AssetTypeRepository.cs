using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Irving.Web.Models;
using System.Data.Entity;

namespace Irving.Web.DAL
{
    public class AssetTypeRepository : Repository<AssetType>
    {
        protected override IQueryable<AssetType> GetIncludes()
        {
            return _dbSet.Include(m => m.Properties)
                         .AsQueryable();
        }

        protected override void AddOrUpdateChildren(AssetType itemToModify)
        {
            base.AddOrUpdateChildren(itemToModify);
            foreach(var property in itemToModify.Properties) 
            {
                if (property.Id <= 0) 
                {
                    _assetTypePropertyRepo.Add(property);
                }
                else 
                {
                    _assetTypePropertyRepo.Update(property);
                    _db.SetAsModified(property);
                }
            }
        }

        #region Constructors
        public AssetTypeRepository()
        {
            _assetTypePropertyRepo = new AssetTypePropertyRepository();
        }

        public AssetTypeRepository(IRepository<AssetTypeProperty> assetTypePropertyRepo)
        {
            _assetTypePropertyRepo = assetTypePropertyRepo;
        }
        #endregion

        #region Variables
        private IRepository<AssetTypeProperty> _assetTypePropertyRepo { get; set; }
        #endregion
    }
}