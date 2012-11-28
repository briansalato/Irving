using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Irving.Web.Models;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using Irving.Web.Helpers;

namespace Irving.Web.DAL
{
    public class CarRepository : Repository<Car>
    {
        #region Repository Overrides
        protected override IQueryable<Car> GetIncludes()
        {
            return _dbSet.Include(car => car.Events)
                         .AsQueryable();
        }

        protected override void AddChildren(Car carToAdd)
        {
            foreach (var eventToAdd in carToAdd.Events)
            {
                _eventRepo.Add(eventToAdd);
            }
        }

        protected override void UpdateChildren(Car carToModify)
        {
            HelperFactory.DataHelper.UpdateChildren(this, _eventRepo, carToModify, (Car c) => c.Events);
        }
        #endregion

        #region Constructors
        [ExcludeFromCodeCoverage]
        public CarRepository()
        {
            _eventRepo = new EventRepository();
        }

        public CarRepository(IIrvingDbContext dbContext, IRepository<Event> eventRepo)
            :base (dbContext)
        {
            _eventRepo = eventRepo;
        }
        #endregion

        #region Variables
        private IRepository<Event> _eventRepo { get; set; }
        #endregion
    }
}