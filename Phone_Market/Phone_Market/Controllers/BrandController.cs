using Microsoft.AspNetCore.Mvc;
using Phone_Market.Code;
using Phone_Market.Models;
using Phone_Market.Services;

namespace Phone_Market.Controllers
{
    public class BrandController : BaseController
    {

        //private readonly Brand_Service brand_Service = new Brand_Service(new UnitOfWork(new Phone_MarketContext()));
        private readonly Brand_Service brand_Service;

        public BrandController(ControllerDependencies dependencies, Brand_Service brand_Service) : base(dependencies) {
            this.brand_Service = brand_Service;
        }

        [HttpGet]
        public IActionResult GetBrands()
        {
            var brands = brand_Service.GetAllBrands();
            return Json(brands);
        }
    }
}
