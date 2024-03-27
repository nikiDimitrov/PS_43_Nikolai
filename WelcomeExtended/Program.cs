using Welcome.Model;
using Welcome.View;
using Welcome.ViewModel;
using Welcome.Others;
using System.Linq.Expressions;
using WelcomeExtended.Others;

namespace Welcome
{
    using static Delegates;
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var user = new User
                {
                    Names = "John Smith",
                    Password = "password123",
                    Role = UserRoleEnum.STUDENT
                };

                var viewModel = new UserViewModel(user);

                var view = new UserView(viewModel);

                view.Display();

                view.DisplayError();
            }
            catch (Exception e)
            {
                var log = new ActionOnError(Log);
                log(e.Message);
            }
            finally
            {
                Console.WriteLine("Executed in any case!");
            }
        }
    }
}