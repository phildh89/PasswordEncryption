using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace PasswordCracker
{
    class Program
    {
        static void Main(string[] args)
        {
            StartUp();
        }
        public static void StartUp()
        {
            Console.WriteLine("\nSingle Thread:");
            string password = EnterPassword();
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            long count = BruteForce(password);

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            Console.WriteLine($"\t{count} attemps");
            string elapsedTime = String.Format($"\t{ts.Hours}:{ts.Minutes}:{ts.Seconds}.{ts.Milliseconds / 10}");
            Console.WriteLine($"\tRunTime {elapsedTime}");

            Console.WriteLine("\nTwo Threads:");
            password = EnterPassword();
            Stopwatch stopWatchMultiThread = new Stopwatch();
            stopWatchMultiThread.Start();

            count = TwoThreadCracker(password);

            stopWatchMultiThread.Stop();
            TimeSpan ts2 = stopWatchMultiThread.Elapsed;

            Console.WriteLine($"\t{count} attemps");
            elapsedTime = String.Format($"\t{ts2.Hours}:{ts2.Minutes}:{ts2.Seconds}.{ts2.Milliseconds / 10}");
            Console.WriteLine($"\tRunTime {elapsedTime}");

            Console.WriteLine("\n\tPress any key to try again");
            Console.ReadKey();
            StartUp();
        }
        public static string EnterPassword()
        {
            Console.Write("\n\tPlease enter a password: ");
            string password = "";
            while (password == "")
            {
                password = Console.ReadLine();
            }
            return password;
        }
        public static long BruteForce(string input)
        {
            long count = 0;

            char[] crackedPassword = new char[input.Length];
            string result = "";
            Random random = new Random();
            while (!String.Equals(input, result))
            {
                count++;
                result = "";
                for (int i = 0; i < input.Length; i++)
                {
                    crackedPassword[i] = Convert.ToChar(random.Next(32,127));
                }
                foreach (char x in crackedPassword)
                {
                    result += x;
                }
            }

            return count;
        }
        public static long TwoThreadCracker(string input)
        {

            string firstHalfInput = "";
            string secondHalfInput = "";

            //Splits password in half to run to seperate BruteForce attemps
            for (int i = 0; i < input.Length / 2; i++)
            {
                firstHalfInput += input[i];
            }
            for (int i = input.Length / 2; i < input.Length; i++)
            {
                secondHalfInput += input[i];
            }
            //multitreading each half to BruteForce
            Task<long> firstHalf = Task<string>.Run(() => BruteForce(firstHalfInput));
            Task<long> secondHalf = Task<string>.Run(() => BruteForce(secondHalfInput));
            Task.WaitAll(firstHalf, secondHalf);
            long firstCount = firstHalf.Result;
            long secondCount = secondHalf.Result;


            return firstCount + secondCount;
        }

    }
}
