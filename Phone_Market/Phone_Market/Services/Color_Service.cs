using Phone_Market.Code;
using Phone_Market.Models;

namespace Phone_Market.Services
{
    public class Color_Service : BaseService
    {
        public Color_Service(ServiceDependencies serviceDependencies) : base(serviceDependencies) 
        {
        }



        public void  CreateColor(Color color)
        {
            UnitOfWork.Colors.Insert(color);
            UnitOfWork.SaveChanges();
        }


        public List<Color> GetAllColors()
        {
            return UnitOfWork.Colors.Get().ToList();
        }

        


    }
}
