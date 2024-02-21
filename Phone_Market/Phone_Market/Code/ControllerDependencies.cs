using Phone_Market.DTO;

namespace Phone_Market.Code
{
    public class ControllerDependencies
    {
        public CurrentUserDTO CurrentUser { get; set; }

        public ControllerDependencies(CurrentUserDTO currentUser)
        {
            this.CurrentUser = currentUser;
        }
    }
}
