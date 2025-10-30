using System;
using ShopManagementSystem.Common;
using ShopManagementSystem.Customer;
using ShopManagementSystem.Product;

namespace ShopManagementSystem.Order
{
    internal class OrderUI
    {
        private OrderService service;
        private CustomerService customerService;

        public OrderUI()
        {
            service = new OrderService();
            customerService = new CustomerService();
        }

        public void Start()
        {
            CreateNewOrder();       
        }

        private void CreateNewOrder()
        {
            Console.Clear();
            NewOrderHeader();
            string customerName;
            while (true)
            {
                Console.Write("Enter Customer's Name: ");
                customerName = Console.ReadLine();
                if (customerName.ToLower() == "exit") return;
                if (customerName == "") continue;
                if (!customerService.Exists(customerName))
                {
                    ConsoleUtiles.PauseForKeyPress("Customer Not Found. Going to Add Customer.");
                    CustomerUI customerUI = new CustomerUI();
                    customerUI.AddCustomerUI();
                }
                break;
            }
            Console.Clear();
            NewOrderHeader();
            CustomerModel customer = customerService.GetCustomerByName(customerName);
            Console.Write("Customer Details:\n\n" +
                          "Name: " + customer.GetName() + ", Phone Number: " + customer.GetPhoneNumber() + ", Age: " + customer.GetAge() + ", Address: " + customer.GetAddress() + "\n\n"
            );
            List<OrderItem> items = new List<OrderItem>();

            while (true)
            {
                string productName;
                while (true)
                {
                    Console.Write("\nEnter Product's Name: ");
                    productName = Console.ReadLine();
                    if (productName.ToLower() == "exit")
                    {
                        if (items.Count > 0)
                        {
                            service.Add(new OrderModel(customerService.GetCustomerByName(customerName), items));
                        }
                        return;
                    }
                    ;
                    if (productName == "") continue;
                    if (!new ProductService().Exists(productName))
                    {
                        Console.Write("\nProduct Not Found.");
                        continue;
                    }
                    break;
                }

                int quantity;
                while (true)
                {
                    Console.Write("Enter Quantity: ");
                    string quantityStr = Console.ReadLine();
                    if (quantityStr.ToLower() == "exit")
                    {
                        if (items.Count != 0)
                        {
                            service.Add(new OrderModel(customerService.GetCustomerByName(customerName), items));
                        }
                        return;
                    }
                    if (quantityStr == "") continue;
                    if (!int.TryParse(quantityStr, out quantity))
                    {
                        Console.WriteLine("\nInvalid Input!\n");
                        continue;
                    }
                    break;
                }
                items.Add(new OrderItem(new ProductModel(new ProductService().GetProductByName(productName)), quantity));
            }
        }
        private void NewOrderHeader()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("---------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n                 CREATE NEW SALE                 \n\n" +

                              "---------------------------------------------------\n\n" +

                                "You can type \"exit\" to go back anytime.\n"
            );
            Console.ForegroundColor = ConsoleColor.Black;
        }
    }
}