using Phone_Market.DTO;

namespace Phone_Market.Code
{
    public class ServiceDependencies
    {
        public UnitOfWork UnitOfWork { get; set; }

        public CurrentUserDTO CurrentUser { get; set; }

        public ServiceDependencies(UnitOfWork unitOfWork, CurrentUserDTO currentUser)
        {
            CurrentUser = currentUser;
            UnitOfWork = unitOfWork;

        }
    }
}
