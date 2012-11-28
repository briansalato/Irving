using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Irving.Web.Models;
using Irving.Web.DAL;

namespace Irving.Web.Controllers
{
    public class CarController : CRUDController<Car>
    {
        #region Constructors
        public CarController()
            :base (new CarRepository())
        {

        }
        public CarController(IRepository<Car> carRepo)
            :base (carRepo)
        {
        }
        #endregion
    }
}
