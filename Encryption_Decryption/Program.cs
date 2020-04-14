using System;

namespace Encryption_Decryption
{
    class Program
    {
        static void Main(string[] args)
        {
            StartUp();
        }
        public static void StartUp()
        {
            string plainText = StringInput("plain text");
            string singleKey = CharInput("a single key");
            string multiKey = StringInput("a multi key");

            Console.WriteLine("\nYou have entered:");
            Console.WriteLine($"\tPlaint Text = {plainText}");
            Console.WriteLine($"\tSingle Key = {singleKey}");
            Console.WriteLine($"\tMulti Key = {multiKey}");

            plainText = CleanText(plainText);
            multiKey = CleanText(multiKey);
            string encryptSingle = SingleKey(plainText, singleKey);
            string encryptMulti = MultiKey(plainText, multiKey);
            string encryptConti = ContinuousKey(plainText, multiKey);

            Console.WriteLine("\nEncrypted message:");
            Console.WriteLine($"\tSingle Key: [{encryptSingle}]");
            Console.WriteLine($"\tMulti Key: [{encryptMulti}]");
            Console.WriteLine($"\tContinuous Key: [{encryptConti}]");
           
            string decryptSingle = SingleDecrypt(encryptSingle, singleKey);
            string decryptMulti = MultiDecrypt(encryptMulti, multiKey);
            string decryptConti = ContinuousDecrypt(encryptConti, multiKey);


            Console.WriteLine("\nDecrypted message:");
            Console.WriteLine($"\tSingle Key: [{decryptSingle}]");
            Console.WriteLine($"\tMulti Key: [{decryptMulti}]");
            Console.WriteLine($"\tContinuous: [{decryptConti}]");

            Console.WriteLine("\n\tPress any key to restart..");
            Console.ReadKey();
            Console.Clear();
            StartUp();
        }
        public static string StringInput(string input)
        {
            string output = "";
            while (output == "")
            {
                try
                {
                    Console.Write($"Enter {input}: ");
                    output = Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            return output;
        }
        public static string CharInput(string input)
        {
            string output= "";
            do
            {
                try
                {
                    Console.Write($"Enter {input}: ");
                    output = Console.ReadLine().ToUpper();
                }
                catch
                {
                    Console.WriteLine("Invalid entry.");
                }
            } while (output.Length != 1 || output[0] < 'A' || output[0] > 'Z');
                

            
            return output;
        }
        public static string CleanText(string input)
        {
            string output = "";
            string temp = input.ToUpper();
            foreach (var x in temp)
            {
                if (x >= 'A' && x <= 'Z')
                {
                    output += x;
                }
            }
            return output;
        }
        public static string SingleKey(string input, string key)
        {
            string encryptedMessage = "";
            int count = key[0] - 'A' + 1;
            foreach (var x in input)
            {
                count = (x + count > 90) ? count - 26 : count;
                encryptedMessage += Convert.ToChar(x + count);
            }

            return encryptedMessage;
        }
        public static string MultiKey(string input, string key)
        {
            string encryptedMessage = "";
            int count = 0;
            int index = 0;
            foreach (var x in input)
            {
                count = key[index] - 'A' + 1;
                count = (x + count > 90) ? count - 26 : count;
                encryptedMessage += Convert.ToChar(x + count);
                index++;
                if (index == key.Length)
                {
                    index = 0;
                }
            }

            return encryptedMessage;
        }
        public static string ContinuousKey(string input, string key)
        {
            string encryptedMessage = "";
            int count = 0;
            int index = 0;
            string temp = input;
            foreach (var x in input)
            {
                count = key[index] - 'A' + 1;
                count = (x + count > 90) ? count - 26: count;
                encryptedMessage += Convert.ToChar(x + count);
                index++;
                if (index == key.Length)
                {
                    key = temp;
                    index = 0;
                }
            }

            return encryptedMessage;
        }
        public static string SingleDecrypt(string input, string key)
        {
            string decryptedMessage = "";
            int count = key[0] - 'A' + 1;
            foreach (var x in input)
            {
                count = (x - count < 65) ? count - 26 : count;
                decryptedMessage += Convert.ToChar(x - count);
            }

            return decryptedMessage;
        }
        public static string MultiDecrypt(string input, string key)
        {
            string decryptedMessage = "";
            int count = 0;
            int index = 0;
            foreach (var x in input)
            {
                count = key[index] - 'A' + 1;
                count = (x - count <65) ? count - 26 : count;
                decryptedMessage += Convert.ToChar(x - count);
                index++;
                if (index == key.Length)
                {
                    index = 0;
                }
            }

            return decryptedMessage;
        }
        public static string ContinuousDecrypt(string input, string key)
        {
            string decryptedMessage = "";
            int count = 0;
            int index = 0;
            string temp = input;
            bool firstLoop = false;
            foreach (var x in input)
            {
                count = key[index] - 'A' + 1;
                count = (x - count < 65) ? count - 26 : count;
                decryptedMessage += Convert.ToChar(x - count);
                index++;
                if (index == key.Length)
                {
                    key = decryptedMessage;
                    index = (firstLoop == false) ? 0 : index;
                    firstLoop = (firstLoop == false) ? true : true;
                }
            }
            return decryptedMessage;
        }

    }
}
