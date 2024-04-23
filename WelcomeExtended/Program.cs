using Welcome.Model;
using Welcome.View;
using Welcome.ViewModel;
using Welcome.Others;
using System.Linq.Expressions;
using WelcomeExtended.Others;
using WelcomeExtended.Data;
using WelcomeExtended.Helpers;
using WelcomeExtended.Loggers;
using Microsoft.Extensions.Logging;

namespace Welcome
{
    using static Delegates;
    internal class Program
    {
        private static int eventId = 0;
        static void Main(string[] args)
        {
            FileLogger successfullyLoggedUsers = new FileLogger("successfullyLogged.txt");
            FileLogger loggingFailedUsers = new FileLogger("notSuccessfullyLogged.txt");
            UserData userData = new UserData();

            User studentUser = new User()
            {
                Name = "student",
                Password = "123",
                Role = UserRoleEnum.STUDENT
            };
            User student1User = new User()
            {
                Name = "Student2",
                Password = "123",
                Role = UserRoleEnum.STUDENT
            };
            User teacherUser = new User()
            {
                Name = "Teacher",
                Password = "1234",
                Role = UserRoleEnum.PROFESSOR
            };
            User adminUser = new User()
            {
                Name = "Admin",
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

            int result = UserHelper.ValidateCredentials(userData, name, password);
            switch(result)
            {
                case 0:
                    LoggerHelper.NotSuccessfulLogin(loggingFailedUsers, eventId++, "Name cannot be empty!");
                    break;
                case 1:
                    LoggerHelper.NotSuccessfulLogin(loggingFailedUsers, eventId++, "Password cannot be empty!");
                    break;
                case 2:
                    LoggerHelper.NotSuccessfulLogin(loggingFailedUsers, eventId++, "Name or password are incorrect!");
                    break;
                case 3:
                    User user = UserHelper.GetUser(userData, name, password);
                    successfullyLoggedUsers.Log(LogLevel.Information, $"{user.Name} succesfully logged!");
                    Console.WriteLine($"{user.Name} succesfully logged!");
                    break;
            }

        }
    }
}