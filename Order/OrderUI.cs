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
            string customerId = ConsoleUtiles.GetInput("Enter Customer's ID: ", "int");
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
                          "ID: " + customer.id + ": Name: " + customer.name + ", Phone Number: " + customer.phoneNumber + ", Age: " + customer.age + ", Address: " + customer.address + "\n\n"
            );
            List<OrderItem> items = new List<OrderItem>();

            while (true)
            {
                string productId = ConsoleUtiles.GetInput("\nEnter Product's ID: ", "int");
                
                if (productId == "exit")
                {
                    if (items.Count > 0)
                    {
                        if (service.Create(new OrderModel(customerService.GetCustomerById(int.Parse(customerId)), items)))
                        {
                            ConsoleUtiles.PauseForKeyPress("Order Created Successfully.");
                        }
                        else
                        {
                            ConsoleUtiles.PauseForKeyPress("Error Creating the order.");
                        }
                    }
                    return;
                }
                if (!new ProductService().Exists(int.Parse(productId)))
                {
                    ConsoleUtiles.PauseForKeyPress("Product not found.");
                    continue;
                }
                string quantityStr = ConsoleUtiles.GetInput("Enter Quantity: ", "int");
                if (quantityStr == "exit")
                {
                    if (items.Count > 0)
                    {
                        if (service.Create(new OrderModel(customerService.GetCustomerById(int.Parse(customerId)), items)))
                        {
                            ConsoleUtiles.PauseForKeyPress("Order Created Successfully.");
                        }
                        else
                        {
                            ConsoleUtiles.PauseForKeyPress("Error Creating the order.");
                        }
                    }
                    return;
                }
                
                items.Add(new OrderItem(new ProductModel(new ProductService().GetProductById(int.Parse(productId))), int.Parse(quantityStr)));
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