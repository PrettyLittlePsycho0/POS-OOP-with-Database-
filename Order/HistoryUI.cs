using ShopManagementSystem.Common;
using ShopManagementSystem.Customer;
using ShopManagementSystem.Product;

namespace ShopManagementSystem.Order
{
    internal class HistoryUI
    {
        private OrderService orderService;
        List<OrderModel> orders;

        public HistoryUI()
        {
            orderService = new OrderService();
            orders = orderService.GetAll();
        }
        public void StartLoop()
        {
            while (true)
            {
                string option = HistoryMenu();
                if (option == "0") break;
                else if (option == "1") ViewAllHistory();
                else if (option == "2")
                {
                    ViewHistoryById();
                }
                else ConsoleUtiles.PauseForKeyPress("Invalid Choice.");
            }
        }
        private string HistoryMenu()
        {
            Console.Clear();
            ViewHistoryHeader();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("1. "); Console.ForegroundColor = ConsoleColor.Black; Console.WriteLine("View All Orders\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("2. "); Console.ForegroundColor = ConsoleColor.Black; Console.WriteLine("View Orders by Customer ID\n");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("0. Go Back to Main Menu\n\n" +
                              "----------------------------------------------------\n"
            );
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("Enter your choice: ");

            return Console.ReadLine();
        }
        private void ViewHistoryHeader()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("----------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n                VIEW ORDER HISTORY                \n\n" +

                              "----------------------------------------------------\n"
            );
            Console.ForegroundColor = ConsoleColor.Black;
        }

        private void ViewAllHistory()
        {
            orders = orderService.GetAll();
            Console.Clear();
            ViewHistoryHeader();
            
            double grandTotal = 0;
            for (int i = 0; i < orders.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Order ID " + orders[i].id + ": Customer: " + orders[i].customer.name + ":" + orders[i].id);
                for (int j = 0; j < orders[i].items.Count; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Red; Console.Write("\t| ");
                    Console.ForegroundColor = ConsoleColor.Black;
                    ProductModel product = orders[i].items[j].product;
                    Console.Write("Product Name: " + product.name + ", Quantity: " + orders[i].items[j].quantity + ", Purchase Price: $" + product.purchasePrice + ", Discount: " + product.discount + "%");
                    Console.ForegroundColor = ConsoleColor.Red; Console.Write(" | ");
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("Total: " + orders[i].items[j].product.CalculateSalePrice() * orders[i].items[j].quantity);
                    grandTotal += orders[i].items[j].product.CalculateSalePrice() * orders[i].items[j].quantity;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Grand Total: " + grandTotal + "\n");
                grandTotal = 0;
            }
            ConsoleUtiles.PauseForKeyPress("");
        }

        private void ViewHistoryById()
        {
            orders = orderService.GetAll();
            Console.Clear();
            ViewHistoryHeader();
            string customerId = ConsoleUtiles.GetInput("Enter Customer's ID: ", "int");
            if (customerId == "exit") return;
            if (!new CustomerService().Exists(int.Parse(customerId)))
            {
                ConsoleUtiles.PauseForKeyPress("Customer Not Found.");
                return;
            }
            double grandTotal = 0;
            for (int i = 0; i < orders.Count; i++)
            {
                if (orders[i].customer.name == customerId)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Order ID " + orders[i].id + ": Customer: " + orders[i].customer.name + ":" + orders[i].id);
                    for (int j = 0; j < orders[i].items.Count; j++)
                    {
                        Console.ForegroundColor = ConsoleColor.Red; Console.Write("\t| ");
                        Console.ForegroundColor = ConsoleColor.Black;
                        ProductModel product = orders[i].items[j].product;
                        Console.Write("Product Name: " + product.name + ", Quantity: " + orders[i].items[j].quantity + ", Purchase Price: $" + product.purchasePrice + ", Discount: " + product.discount + "%");
                        Console.ForegroundColor = ConsoleColor.Red; Console.Write(" | ");
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine("Total: " + orders[i].items[j].product.CalculateSalePrice() * orders[i].items[j].quantity);
                        grandTotal += orders[i].items[j].product.CalculateSalePrice() * orders[i].items[j].quantity;
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Grand Total: " + grandTotal + "\n");
                    grandTotal = 0;
                }
            }
            ConsoleUtiles.PauseForKeyPress("");
        }
    }
}