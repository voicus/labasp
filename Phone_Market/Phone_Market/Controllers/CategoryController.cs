using Microsoft.AspNetCore.Mvc;
using Phone_Market.Code;
using Phone_Market.Models;
using Phone_Market.Services;

namespace Phone_Market.Controllers
{
    public class CategoryController : BaseController
    {

        private readonly Category_Service category_Service;



        public CategoryController(ControllerDependencies dependencies, Category_Service category_Service) : base(dependencies) { 
            this.category_Service = category_Service;
        }

        [HttpGet]
        public IActionResult GetCategories() 
        {
            var categoryList = category_Service.GetAllCategories();
            return Json(categoryList);

        }
    }
}
