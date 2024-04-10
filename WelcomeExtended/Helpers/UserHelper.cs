using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Welcome.Model;
using WelcomeExtended.Data;

namespace WelcomeExtended.Helpers
{
    public static class UserHelper
    {
        public static void ToString(this User user) { }

        public static void ValidateCredentials(UserData userData, string name, string password)
        {
            if(String.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty!");
            }
            else if(String.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Password cannot be empty!");
            }
            else
            {
                userData.ValidateUser(name, password);
            }
        }

        public static void GetUser(UserData userData, string name, string password)
        {
            userData.GetUser(name, password);
        }

    }
}
