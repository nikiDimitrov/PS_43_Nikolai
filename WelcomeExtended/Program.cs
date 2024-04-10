using Welcome.Model;
using Welcome.View;
using Welcome.ViewModel;
using Welcome.Others;
using System.Linq.Expressions;
using WelcomeExtended.Others;
using WelcomeExtended.Data;
using WelcomeExtended.Helpers;

namespace Welcome
{
    using static Delegates;
    internal class Program
    {
        static void Main(string[] args)
        {
            UserData userData = new UserData();

            User studentUser = new User()
            {
                Names = "student",
                Password = "123",
                Role = UserRoleEnum.STUDENT
            };
            User student1User = new User()
            {
                Names = "Student2",
                Password = "123",
                Role = UserRoleEnum.STUDENT
            };
            User teacherUser = new User()
            {
                Names = "Teacher",
                Password = "1234",
                Role = UserRoleEnum.PROFESSOR
            };
            User adminUser = new User()
            {
                Names = "Admin",
                Password = "12345",
                Role = UserRoleEnum.ADMIN
            };
            userData.AddUser(studentUser);
            userData.AddUser(student1User);
            userData.AddUser(teacherUser);
            userData.AddUser(adminUser);

            Console.WriteLine("Enter name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Enter password: ");
            string password = Console.ReadLine();

            UserHelper.ValidateCredentials(userData, name, password);
        }
    }
}