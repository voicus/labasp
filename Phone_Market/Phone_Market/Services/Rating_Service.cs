using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Phone_Market.Code;
using Phone_Market.DTO;
using Phone_Market.Models;

namespace Phone_Market.Services
{
    public class Rating_Service : BaseService
    {
        private readonly Product_Service product_Service;
        public Rating_Service(ServiceDependencies serviceDependencies, Product_Service product_Service) : base(serviceDependencies)
        {
            this.product_Service = product_Service;
        }

        public GiveRatingModel GetGiveRatingModel(Guid productId)
        {
            var product = product_Service.GetProductById(productId);
            var ratingModel = new GiveRatingModel
            {
                ProductId = productId,
                ProductName = product.Name,
                IsRated = false
            };
            if (UnitOfWork.ProductRatings.Get().Where(r => r.ProductId == productId && r.UserId == CurrentUser.Id).Any())
            {
                var rating = UnitOfWork.ProductRatings.Get().Where(r => r.ProductId == productId && r.UserId ==  CurrentUser.Id).FirstOrDefault();
                ratingModel.Rating = rating.Rating;
                ratingModel.Comment = rating.Description;
                ratingModel.IsRated = true;
            }
            return ratingModel;
        }

        public void CreateRating(GiveRatingModel ratingModel)
        {
            var rating = new ProductRating
            {
                ProductId = ratingModel.ProductId,
                UserId = CurrentUser.Id,
                Rating = ratingModel.Rating,
                Description = ratingModel.Comment
            };
            UnitOfWork.ProductRatings.Insert(rating);
            UnitOfWork.SaveChanges();

        }

        public void UpdateRating(GiveRatingModel ratingModel)
        {
            var rating = UnitOfWork.ProductRatings.Get().Where(r => r.ProductId == ratingModel.ProductId && r.UserId == CurrentUser.Id).FirstOrDefault();
            rating.Rating = ratingModel.Rating;
            rating.Description = ratingModel.Comment;
            UnitOfWork.ProductRatings.Update(rating);
            UnitOfWork.SaveChanges();
        }
    }
}
