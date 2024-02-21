using Phone_Market.Code;
using Phone_Market.Models;

namespace Phone_Market.Services
{
    public class Category_Service : BaseService
    {

        public Category_Service(ServiceDependencies serviceDependencies) : base(serviceDependencies) 
        {

        }

        public List<Category> GetAllCategories()
        {
            return UnitOfWork.Categories.Get().ToList();
        }
    }
}
