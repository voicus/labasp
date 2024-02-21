using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using Phone_Market.Code;
using Phone_Market.DTO;
using Phone_Market.Enums;
using Phone_Market.Extensions;
using Phone_Market.Models;

namespace Phone_Market.Services
{
    public class Account_Service : BaseService
    {
        public Account_Service(ServiceDependencies dependencies) : base(dependencies)
        {
        }
        public CurrentUserDTO GetUserByEmailAndPassword(LoginModel loginModel)
        {
            var user = UnitOfWork.Users.Get().SingleOrDefault(u => u.Email == loginModel.Email && u.Password == loginModel.Password.Sha256());
            if (user == null)
            {
                return null;
            }
            CurrentUserDTO currentUserDto = new CurrentUserDTO
            {
                Email = loginModel.Email,
                FullName = user.FirstName + " " + user.LastName,
                Id = user.Id,
                RoleId = user.RoleId,
            };

            return currentUserDto;
            
        }

        public bool IsAccount(string email)
        {
            var user = UnitOfWork.Users.Get().SingleOrDefault(u => u.Email == email);
            if (user == null) { return false; }
            return true;
        }

        public void RegisterUser(RegisterUserModel registerUserModel)
        { 
            var user = new User
            {
                UserName = registerUserModel.UserName,
                FirstName = registerUserModel.FirstName,
                LastName = registerUserModel.LastName,
                RoleId = (int)RoleType.User,
                Email = registerUserModel.Email,
                Password = registerUserModel.Password.Sha256(),
                Id = Guid.NewGuid()
            };
            UnitOfWork.Users.Insert(user);
            UnitOfWork.SaveChanges();
        }
        public bool IsUserPasswordCorrect(Guid userId, string password)
        {
            var user = UnitOfWork.Users.Get().SingleOrDefault(u => u.Id == userId);
            return user.Password == password.Sha256();
        }

        public List<ProductDto> GetMyFavourites(Guid userId)
        {
            var user = UnitOfWork.Users.Get().Include(u => u.Products).SingleOrDefault(u => u.Id == userId);
            var products = UnitOfWork.Products.Get()
                .Include(x => x.Brand)
                .Include(x => x.Images)
                .Include(x => x.Color)
                .Include(x => x.Category)
               .ToList();


            var productsDto = new List<ProductDto>();

            foreach (var productRaw in user.Products)
            {
                var product = UnitOfWork.Products.Get()
                    .Include(x => x.Brand)
                    .Include(x => x.Images)
                    .Include(x => x.Color)
                    .Include(x => x.Category).FirstOrDefault(x => x.Id == productRaw.Id);
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
                    CoverImage = product.Images.FirstOrDefault().Picture,
                    Discount = product.Discount
                };



                productsDto.Add(productDto);
            }
            return productsDto;
        }
        public MyUserProfileModel GetMyUserProfileModelById(Guid userId)
        {
            var user = UnitOfWork.Users.Get().SingleOrDefault(u => u.Id == userId);
            var myUserProfileModel = new MyUserProfileModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName
            };
            return myUserProfileModel;
        }

        public void UpdateMyUserProfile(MyUserProfileModel myUserProfileModel)
        {
            //myUserValidator.Validate(myUserProfileModel).ThenThrow(myUserProfileModel);
            var user = UnitOfWork.Users.Get().SingleOrDefault(u => u.Id == myUserProfileModel.Id);
            if (user.Password == myUserProfileModel.CurrentPassword.Sha256())
            {
                user.UserName = myUserProfileModel.UserName;
                if (myUserProfileModel.NewPassword != null)
                {
                    user.Password = myUserProfileModel.NewPassword.Sha256();
                }
                UnitOfWork.Users.Update(user);
                UnitOfWork.SaveChanges();
            }
        }

        public string GetUserMailById(Guid id)
        {
            var user = UnitOfWork.Users.Get().SingleOrDefault(u => u.Id == id);
            var mail = user.Email;
            return mail;
        }

    }
}
