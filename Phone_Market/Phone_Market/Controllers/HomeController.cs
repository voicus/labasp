using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Phone_Market.Code;
using Phone_Market.Models;
using Phone_Market.Services;
using System.Diagnostics;

namespace Phone_Market.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        private readonly Color_Service color_Service;

        private readonly Product_Service product_service;

        public HomeController(ControllerDependencies dependencies, Color_Service color_Service, Product_Service product_Service) : base(dependencies) { 
            this.color_Service = color_Service;
            this.product_service = product_Service;
        }

        public IActionResult Index(string filter = "")
        {
            var products = product_service.GetAllProducts(filter);
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}