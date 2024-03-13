using Welcome.Model;
using Welcome.View;
using Welcome.ViewModel;
using Welcome.Others;

namespace Welcome
{
    internal class Program
    {
        static void Main(string[] args)
        {
            User user = new User();
            user.Names = "Nikolai Dimitrov";
            user.Password = "123456789";
            user.Role = UserRoleEnum.ADMIN;

            UserViewModel viewModel = new UserViewModel(user);

            UserView userView = new UserView(viewModel);
            userView.Display();
        }
    }
}