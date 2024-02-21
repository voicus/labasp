using Phone_Market.DTO;

namespace Phone_Market.Code
{
    public class BaseService
    {
        public readonly CurrentUserDTO CurrentUser;
        public readonly UnitOfWork UnitOfWork;


        public BaseService(ServiceDependencies serviceDependencies)
        {
            CurrentUser = serviceDependencies.CurrentUser;
            UnitOfWork = serviceDependencies.UnitOfWork;

        }
    }
}
