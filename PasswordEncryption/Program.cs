using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace PasswordEncryption
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n\t\tPASSWORD AUTHENTICATION SYSTEM");
            StartUp();
        }
        public static void StartUp()
        {
            Console.WriteLine("\n\t\tPlease Select one option:");
            Console.WriteLine("\t\t1. Establist an account");
            Console.WriteLine("\t\t2. Authenticate a user");
            Console.WriteLine("\t\t3. Exit the system");
            int selection=0;

            do
            {
                try
                {
                    Console.Write("\n\t\t Enter selection: ");
                    selection = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    continue;
                }
            } while (selection != 1 && selection != 2 && selection != 3);
            if (selection == 1)
            {
                CreateAcc();
                StartUp();
            }
            else if (selection == 2)
            {
                AuthenticateUser();
                StartUp();
            }
            else if (selection == 3)
            {
                Environment.Exit(0);
            }
        }
        public static void CreateAcc()
        {
            Console.Clear();
            Console.WriteLine("\n\t\tCreating an account");
            Console.Write("\n\t\tUser Name: ");
            string userName = Console.ReadLine();
            Console.Write("\n\t\tPassword: ");
            string password = Console.ReadLine();
            Console.Clear();
            Console.Write("\n\t\t");
            Console.WriteLine(Storage.AddAcc(userName, password));
        }
        public static void AuthenticateUser()
        {
            Console.Clear();
            Console.WriteLine("\n\t\tAuthenticate an account");
            Console.Write("\n\t\tUser Name: ");
            string userName = Console.ReadLine();
            Console.Write("\n\t\tPassword: ");
            string password = Console.ReadLine();
            Console.Clear();
            Console.Write("\n\t\t");
            Console.WriteLine(Storage.Authenticate(userName, password));
        }
    }
    public class Storage
    {
        private static Dictionary<string, byte[]> _accInfo = new Dictionary<string, byte[]>();
        public static string AddAcc(string userName, string password)
        {
            if (_accInfo.ContainsKey(userName))
            {
                return "The user name you have entered is already taken.";
            }
            else
            {
                _accInfo.Add(userName, HashString(password));
                return "Account Created.";
            }
        }
        public static string Authenticate(string userName, string password)
        {
            string compare1 = "";
            string compare2 = "";
            //Check for username
            if (_accInfo.ContainsKey(userName))
            {
                foreach (var x in _accInfo[userName])
                {
                    compare1 += x;
                }
                foreach (var x in HashString(password))
                {
                    compare2 += x;
                }
                //Check for matching password
                if (compare1 ==compare2)
                {
                    return "Account Authentication Successful";
                }
                else
                {
                    return "Incorrect Password";
                }
            }
            else
            {
                return "User Name does not exist";
            }


        }
        private static byte[] HashString(string input)
        {
            byte[] encryptedString;
            using (SHA256 mySHA256 = SHA256.Create())
            {
                UnicodeEncoding ue = new UnicodeEncoding();
                byte[] stringBytes = ue.GetBytes(input);
                encryptedString = mySHA256.ComputeHash(stringBytes);
            }
            return encryptedString;
        }
    }
}
