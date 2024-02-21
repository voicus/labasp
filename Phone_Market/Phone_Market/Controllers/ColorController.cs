using Microsoft.AspNetCore.Mvc;
using Phone_Market.Code;
using Phone_Market.Models;
using Phone_Market.Services;

namespace Phone_Market.Controllers
{
    public class ColorController : BaseController
    {


        private readonly Color_Service color_Service;

        public ColorController(ControllerDependencies dependencies, Color_Service color_Service) : base(dependencies) { 
            this.color_Service = color_Service;
        }
        
        [HttpGet]
        public IActionResult GetColors()
        {
            var colorList = color_Service.GetAllColors();
            return Json(colorList);
        }
    }
}
