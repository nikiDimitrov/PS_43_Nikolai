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

        public static int ValidateCredentials(UserData userData, string name, string password)
        {
            if(String.IsNullOrWhiteSpace(name))
            {
                return 0;
            }
            else if(String.IsNullOrWhiteSpace(password))
            {
                return 1;
            }
            else if(!userData.ValidateUser(name, password))
            {
                return 2;
            }
            return 3;
        }

        public static User? GetUser(UserData userData, string name, string password)
        {
            return userData.GetUser(name, password);
        }

    }
}
