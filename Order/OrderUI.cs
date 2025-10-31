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
            string customerId = ConsoleUtiles.GetInput("Enter Customer's Name: ", "string");
            if (customerId == "exit") return;
            if (!customerService.Exists(int.Parse(customerId)))
            {
                ConsoleUtiles.PauseForKeyPress("Customer Not Found. Going to Add Customer.");
                CustomerUI customerUI = new CustomerUI();
                customerUI.AddCustomerUI();
            }
            
            Console.Clear();
            NewOrderHeader();
            CustomerModel customer = customerService.GetCustomerById(int.Parse(customerId));
            Console.Write("Customer Details:\n\n" +
                          "ID: " + customer.id + ", Name: " + customer.GetName() + ", Phone Number: " + customer.GetPhoneNumber() + ", Age: " + customer.GetAge() + ", Address: " + customer.GetAddress() + "\n\n"
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
                            service.Create(new OrderModel(customerService.GetCustomerById(int.Parse(customerId)), items));
                        }
                        return;
                    }
                    ;
                    if (productName == "") continue;
                    
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
                            service.Create(new OrderModel(customerService.GetCustomerById(int.Parse(customerId)), items));
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
                items.Add(new OrderItem(new ProductModel(new ProductService().GetCustomerById(int.Parse(productName))), quantity));
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