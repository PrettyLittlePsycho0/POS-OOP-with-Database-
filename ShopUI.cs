using ShopManagementSystem.Customer;
using ShopManagementSystem.Product;
using ShopManagementSystem.Order;
using ShopManagementSystem.Common;

namespace ShopManagementSystem
{
    internal class ShopUI
    {
        private ProductUI productUI;
        private CustomerUI customerUI;
        private OrderUI orderUI;
        private HistoryUI historyUI;

        public ShopUI()
        {
            productUI = new ProductUI();
            customerUI = new CustomerUI();
            historyUI = new HistoryUI();
            orderUI = new OrderUI();
        }
        public void StartLoop()
        {
            while (true)
            {
                string option = MainMenu();
                if (option == "0") break;
                else if (option == "1") productUI.StartLoop();
                else if (option == "2") customerUI.StartLoop();
                else if (option == "3") orderUI.Start();
                else if (option == "4") historyUI.StartLoop();
                else ConsoleUtiles.PauseForKeyPress("Invalid Input!");
            }
        }
        private string MainMenu()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.WriteLine("===================================================");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n              SHOP MANGEMENT SYSTEM              \n\n" +

                              "===================================================\n"
            );
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("1. "); Console.ForegroundColor = ConsoleColor.Black; Console.WriteLine("Product Management\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("2. "); Console.ForegroundColor = ConsoleColor.Black; Console.WriteLine("Customer Management\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("3. "); Console.ForegroundColor = ConsoleColor.Black; Console.WriteLine("Create New Sale (Order)\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("4. "); Console.ForegroundColor = ConsoleColor.Black; Console.WriteLine("View Order History\n");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("0. Exit Application\n\n" +
                              "---------------------------------------------------\n"
            );
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("Enter your choice: ");
            return Console.ReadLine();
        }
    }
}