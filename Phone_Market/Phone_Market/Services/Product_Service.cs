using Microsoft.EntityFrameworkCore;
using Phone_Market.Code;
using Phone_Market.DTO;
using Phone_Market.Models;

namespace Phone_Market.Services
{
    public class Product_Service : BaseService
    {


        public Product_Service(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
        }


        public void CreateProduct(ProductCreateModel productCreateModel)
        {

            var images = productCreateModel.Images;

            List<byte[]> productImages = new List<byte[]>();
            List<int> colorsIds = new List<int>();


            foreach (var image in images)
            {

                using (var ms = new MemoryStream())
                {
                    image.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    productImages.Add(fileBytes);
                }

            }



            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = productCreateModel.Name,
                Description = productCreateModel.Description,
                Price = productCreateModel.Price,
                BrandId = productCreateModel.BrandId,
                ColorId = productCreateModel.ColorId,
                CategoryId = productCreateModel.CategoryId,
                Discount = 0
            };

            foreach (var image in productImages)
            {
                var picture = new Image
                {
                    Picture = image,
                    ProductId = product.Id
                };

                UnitOfWork.Images.Insert(picture);
            }
            UnitOfWork.Products.Insert(product);
            UnitOfWork.SaveChanges();

        }

        private bool shouldBeFiltered(string name, string[] keyWords)
        {
            return keyWords.All(term => name.Contains(term));
        }


        public List<ProductDto> GetAllProducts(string filter)
        {
            var keyWords = filter.Split(' ');
            var products = UnitOfWork.Products.Get()
                .Include(x => x.Brand)
                .Include(x => x.Images)
                .Include(x => x.Color)
                .Include(x => x.Category)
                .Where(x => true);
                //.Where(x => filter == "" || shouldBeFiltered(x.Name, keyWords))
                //.ToList();
            foreach(var kw in keyWords)
            {
                products = products.Where(x => x.Name.ToUpper().Contains(kw));
            }

            var prod = products.ToList();

            var productsDto = new List<ProductDto>();

            foreach (var product in prod)
            {
                var ratings = UnitOfWork.ProductRatings.Get().Where(r => r.ProductId == product.Id).ToList();
                double total = 0;
                foreach (var r in ratings)
                {
                    total += r.Rating;
                }
                total /= ratings.Count;
                var productDto = new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    BrandId = product.BrandId,
                    Description = product.Description,
                    Price = product.Price,
                    Brand = product.Brand.Name,
                    Color = product.Color.Name,
                    Category = product.Category.Name,
                    CategoryId = product.Category.Id,
                    Images = product.Images.Select(x => x.Picture).ToList(),
                    CoverImage = product.Images.FirstOrDefault()!.Picture,
                    Discount = product.Discount,
                    AverageRating = Math.Round(total, 0, MidpointRounding.ToEven)
                };



                productsDto.Add(productDto);
            }
            return productsDto;
        }

        public void AddFavouriteProduct(Guid eventId, Guid userId)
        {
            var user = GetUserById(userId);
            var product = GetProductById(eventId);
            user.Products.Add(product);
            UnitOfWork.SaveChanges();

        }

        public void RemoveFavouriteProduct(Guid eventId, Guid userId)
        {
            var user = GetUserById(userId);
            var product = GetProductById(eventId);
            user.Products.Remove(product);
            UnitOfWork.SaveChanges();
        }
        public User GetUserById(Guid userId)
        {
            return UnitOfWork.Users.Get().Include(u => u.Products).FirstOrDefault(x => x.Id == userId);
        }
        public Product GetProductById(Guid productId)
        {
            return UnitOfWork.Products.Get().FirstOrDefault(x => x.Id == productId);
        }

        public bool GetFavouriteProducts(Guid userId, Guid productId)
        {
            var user = GetUserById(userId);
            var nr = user.Products.Where(x => x.Id == productId).Count();
            return nr == 1;
        }


        public ProductDto GetProductWithDetails(Guid id)
        {
            var product = UnitOfWork.Products.Get()
                .Include(x => x.Brand)
                .Include(x => x.Images)
                .Include(x => x.Color)
                .Include(x => x.Category)
                .FirstOrDefault(x => x.Id == id);

            var ratings = UnitOfWork.ProductRatings.Get().Where(r => r.ProductId == id).ToList();
            var ratingsDTO = new List<GiveRatingModel>();
            foreach (var rating in ratings)
            {
                var user = UnitOfWork.Users.Get().Where(u => u.Id == rating.UserId).FirstOrDefault();
                var ratingDto = new GiveRatingModel
                {
                    Rating = rating.Rating,
                    Comment = rating.Description,
                    UserName = user.UserName,

                };
                ratingsDTO.Add(ratingDto);
            }
            var productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                BrandId = product.BrandId,
                Description = product.Description,
                Price = product.Price,
                Brand = product.Brand.Name,
                Color = product.Color.Name,
                Category = product.Category.Name,
                CategoryId = product.Category.Id,
                Images = product.Images.Select(x => x.Picture).ToList(),
                CoverImage = product.Images.FirstOrDefault()!.Picture,
                Discount = product.Discount,
                Ratings = ratingsDTO
            };

            return productDto;
        }

        public IEnumerable<ShoppingCartDto> GetCart(Guid Id)
        {
            var cart = UnitOfWork.ShoppingCarts.Get().Where(x => x.UserId == Id).ToList();

            var cartDto = new List<ShoppingCartDto>();
            foreach(var item in cart)
            {

                var productDto = UnitOfWork.Products.Get().Where(x => x.Id == item.ProductId).FirstOrDefault();

                var product = new ShoppingCartDto
                {
                    ProductId = item.ProductId,
                    ProductName = productDto.Name,
                    ProductPrice = productDto.Price,
                    ProductDiscount = productDto.Discount,
                    ProductImage = UnitOfWork.Images.Get().Where(x => x.ProductId == item.ProductId).FirstOrDefault().Picture,
                };
                cartDto.Add(product);

            }

            return cartDto;

        }

        public void AddToCart(Guid productId, Guid userId)
        {
            var user = GetUserById(userId);
            var product = GetProductById(productId);
            var cart = new ShoppingCart
            {
                UserId = userId,
                ProductId = productId,
                Quantity = 1,
            };

            var alreadyExist = UnitOfWork.ShoppingCarts.Get().Where(x => x.UserId == userId && x.ProductId == productId).FirstOrDefault();
            if (alreadyExist == null)
            {
                UnitOfWork.ShoppingCarts.Insert(cart);
                UnitOfWork.SaveChanges();
            }
        }

        public void RemoveItem(Guid productId, Guid userId)
        {
            var user = GetUserById(userId);
            var product = GetProductById(productId);
            var cart = UnitOfWork.ShoppingCarts.Get().Where(x => x.UserId == userId && x.ProductId == productId).FirstOrDefault();
            UnitOfWork.ShoppingCarts.Delete(cart);
            UnitOfWork.SaveChanges();
        }


        public double GetTotalPrice(Guid userid)
        {
            var cart = UnitOfWork.ShoppingCarts.Get().Where(x => x.UserId.ToString() == userid.ToString());
            double TotalPrice = 0;
            foreach (var item in cart)
            {
                TotalPrice = (double)(TotalPrice + (item.Quantity * (item.Product.Price - item.Product.Price * item.Product.Discount / 100)));
            }
            return TotalPrice;
        }

        public void AddReceipt(Receipt receipt)
        {
            UnitOfWork.Receipts.Insert(receipt);
            UnitOfWork.SaveChanges();
        }



        public void AddOrderedItem(OrderedItem orderedItem, int Id)
        {
            var exist = UnitOfWork.OrderedItems.Get().FirstOrDefault(x => x.ProductId == orderedItem.ProductId && x.OrderId == Id);
            UnitOfWork.OrderedItems.Insert(orderedItem);
            UnitOfWork.SaveChanges();
        }


        public void RemoveItemFromCart(Guid productId,Guid userid)
        {
           
            var tem = UnitOfWork.ShoppingCarts.Get().FirstOrDefault(x => x.ProductId == productId  && x.UserId.ToString() == userid.ToString());
            UnitOfWork.ShoppingCarts.Delete(tem);
            UnitOfWork.SaveChanges();
        }



        public EditProductDto GetEditProductDto(Guid id)
        {
            var product = UnitOfWork.Products.Get().FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            var editProductDto = new EditProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = (double)product.Price,
                Discount = product.Discount,

            };
            return editProductDto;
        }

        public void EditProduct(EditProductDto model)
        {

            var product = UnitOfWork.Products.Get().FirstOrDefault(x => x.Id == model.Id);
            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.Discount = model.Discount;
            UnitOfWork.Products.Update(product);
            UnitOfWork.SaveChanges();

        }

        public void DeleteProduct(Guid id)
        {
            var product = UnitOfWork.Products.Get().FirstOrDefault(x => x.Id == id);
            UnitOfWork.Products.Delete(product);
            UnitOfWork.SaveChanges();
        }
    }
}
