using Phone_Market.Code;
using Phone_Market.Models;

namespace Phone_Market.Services
{
    public class Brand_Service : BaseService
    {

        public Brand_Service(ServiceDependencies serviceDependencies) : base(serviceDependencies) 
        {
            
        }

        public List<Brand> GetAllBrands()
        {

            return UnitOfWork.Brands.Get().ToList();
        }



    }
}
