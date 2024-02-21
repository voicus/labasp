using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Phone_Market.Code;
using Phone_Market.DTO;
using Phone_Market.Services;

namespace Phone_Market.Controllers
{
    public class RatingController : BaseController
    {
        private readonly Rating_Service rating_Service;
        public RatingController(ControllerDependencies dependencies, Rating_Service rating_Service) : base(dependencies)
        {
            this.rating_Service = rating_Service;
        }

        [Authorize(Policy = "User")]
        [HttpGet]
        public IActionResult RateProduct(Guid productId)
        {
            var giveRatingModel = rating_Service.GetGiveRatingModel(productId);
            return View(giveRatingModel);
        }

        [Authorize(Policy = "User")]
        [HttpPost]
        public IActionResult RateProduct(GiveRatingModel model)
        {
            if (model.Rating == 0)
            {
                return View(rating_Service.GetGiveRatingModel(model.ProductId));
            }
            if (model.IsRated == true)
            {
                rating_Service.UpdateRating(model);
            }
            else
            {
                rating_Service.CreateRating(model);
            }
            return RedirectToAction("Index", "Home");

        }
    }
}
