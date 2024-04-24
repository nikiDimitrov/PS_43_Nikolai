using Welcome.Model;
using Welcome.View;
using Welcome.ViewModel;
using Welcome.Others;
using DataLayer.Database;
using DataLayer.Model;
namespace Welcome
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var context = new DatabaseContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();



                var logger = new LoggerToDatabase(context);
                bool exit = false;
                while (!exit)
                {
                    Console.WriteLine("1. Get all users");
                    Console.WriteLine("2. Add new user");
                    Console.WriteLine("3. Delete user");
                    Console.WriteLine("4. Exit");

                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            GetAllUsers(context, logger);
                            break;
                        case "2":
                            AddUser(context, logger);
                            break;
                        case "3":
                            DeleteUser(context, logger);
                            break;
                        case "4":
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                var loggers = context.LogEntries.ToList();
                foreach (var log in loggers)
                {
                    Console.WriteLine($"Logger: [{log.Id}], [{log.Message}], [{log.CreatedAt}]");
                }
            }
            static void GetAllUsers(DatabaseContext context, LoggerToDatabase logger)
            {
                var users = context.Users.ToList();
                foreach (var user in users)
                {
                    Console.WriteLine($"Name: {user.Name}, Role: {user.Role}");
                }
                logger.LogMessage("All users are in local list!");
            }

            static void AddUser(DatabaseContext context, LoggerToDatabase logger)
            {
                Console.WriteLine("Enter name:");
                string name = Console.ReadLine();
                Console.WriteLine("Enter password:");
                string password = Console.ReadLine();
                Console.WriteLine("Enter email:");
                string email = Console.ReadLine();
                Console.WriteLine("Enter faculty number:");
                string facultyNumber = Console.ReadLine();

                var user = new DatabaseUser { Name = name, Password = password, Email = email, FacultyNumber = facultyNumber };
                context.Users.Add(user);
                context.SaveChanges();

                Console.WriteLine("User has been added successfully.");
                logger.LogMessage($"The user has been added: {name}");
            }

            static void DeleteUser(DatabaseContext context, LoggerToDatabase logger)
            {
                Console.WriteLine("Enter the name of the user to delete:");
                string name = Console.ReadLine();

                var user = context.Users.FirstOrDefault(u => u.Name == name);
                if (user != null)
                {
                    context.Users.Remove(user);
                    context.SaveChanges();
                    Console.WriteLine("The user has been deleted successfully.");
                    logger.LogMessage($"The user has been deleted successfully: {name}");
                }
                else
                {
                    Console.WriteLine("No user found with that name.");
                    logger.LogMessage($"The user {name} couldn't be found!");
                }
            }
        }
    }
}