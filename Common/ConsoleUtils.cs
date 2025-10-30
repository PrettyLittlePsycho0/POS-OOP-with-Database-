using Microsoft.VisualBasic;

namespace ShopManagementSystem.Common
{
    internal static class ConsoleUtiles
    {
        public static void PauseForKeyPress(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\n" + message + " Press any key to continue...");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.ReadKey();
        }
        public static string GetInput(string prompt, string type)
        {
            while (true)
            {
                Console.Write(prompt);
                string str = Console.ReadLine();

                if (str == null) continue;
                if (str.ToLower() == "exit") return "exit";
                if (str.Trim() == "") continue;
                if (type == "string") return str;
                
                if (type == "double")
                {
                    if (!double.TryParse(str, out _))
                    {
                        Console.WriteLine("\nInvalid Input!");
                        continue;
                    }
                }
                else if (type == "int")
                {
                    if (!int.TryParse(str, out _))
                    {
                        Console.WriteLine("\nInvalid Input!");
                        continue;
                    }
                }
                else if (type == "char")
                {
                    if (str.Length != 1)
                    {
                        Console.WriteLine("\nPlease enter a single character!");
                        continue;
                    }
                }
                return str;
            }
        }


    }
}