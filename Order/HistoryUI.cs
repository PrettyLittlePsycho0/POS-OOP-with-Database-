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
                    ViewHistoryByName();
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
            Console.Write("2. "); Console.ForegroundColor = ConsoleColor.Black; Console.WriteLine("View Orders by Customer Name\n");
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
                Console.WriteLine(i + 1 + ". Customer: " + orders[i].GetCustomer().GetName());
                for (int j = 0; j < orders[i].GetOrderItems().Count; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Red; Console.Write("\t| ");
                    Console.ForegroundColor = ConsoleColor.Black;
                    ProductModel product = orders[i].GetOrderItems()[j].GetProduct();
                    Console.Write("Product Name: " + product.GetName() + ", Quantity: " + orders[i].GetOrderItems()[j].quantity + ", Purchase Price: " + product.GetPurchasePrice() + ", Discount: " + product.GetDiscount());
                    Console.ForegroundColor = ConsoleColor.Red; Console.Write(" | ");
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("Total: " + orders[i].GetOrderItems()[j].GetProduct().CalculateSalePrice() * orders[i].GetOrderItems()[j].quantity);
                    grandTotal += orders[i].GetOrderItems()[j].GetProduct().CalculateSalePrice() * orders[i].GetOrderItems()[j].quantity;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Grand Total: " + grandTotal + "\n");
                grandTotal = 0;
            }
            ConsoleUtiles.PauseForKeyPress("");
        }

        private void ViewHistoryByName()
        {
            orders = orderService.GetAll();
            Console.Clear();
            ViewHistoryHeader();
            string customerName;
            while (true)
            {
                Console.Write("Enter Customer's Name: ");
                customerName = Console.ReadLine();
                if (customerName.ToLower() == "exit") return;
                if (customerName == "") continue;
                if (!new CustomerService().Exists(int.Parse(customerName)))
                {
                    ConsoleUtiles.PauseForKeyPress("Customer Not Found. Going to Add Customer.");
                    CustomerUI customerUI = new CustomerUI();
                    customerUI.AddCustomerUI();
                }
                break;
            }
            double grandTotal = 0;
            for (int i = 0; i < orders.Count; i++)
            {
                if (orders[i].GetCustomer().GetName() == customerName)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(i + 1 + ". Customer: " + orders[i].GetCustomer().GetName());
                    for (int j = 0; j < orders[i].GetOrderItems().Count; j++)
                    {
                        Console.ForegroundColor = ConsoleColor.Red; Console.Write("\t| ");
                        Console.ForegroundColor = ConsoleColor.Black;
                        ProductModel product = orders[i].GetOrderItems()[j].GetProduct();
                        Console.Write("Product Name: " + product.GetName() + ", Quantity: " + orders[i].GetOrderItems()[j].quantity + ", Purchase Price: " + product.GetPurchasePrice() + ", Discount: " + product.GetDiscount());
                        Console.ForegroundColor = ConsoleColor.Red; Console.Write(" | ");
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine("Total: " + orders[i].GetOrderItems()[j].GetProduct().CalculateSalePrice() * orders[i].GetOrderItems()[j].quantity);
                        grandTotal += orders[i].GetOrderItems()[j].GetProduct().CalculateSalePrice() * orders[i].GetOrderItems()[j].quantity;
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