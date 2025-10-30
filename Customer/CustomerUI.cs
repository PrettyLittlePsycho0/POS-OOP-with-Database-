using System;
using ShopManagementSystem.Common;

namespace ShopManagementSystem.Customer
{
    internal class CustomerUI
    {
        private CustomerService service;

        public CustomerUI()
        {
            service = new CustomerService(); 
        }
        public void StartLoop()
        {
            while (true)
            {
                string option = CustomerMenu();
                if (option == "0") break;
                else if (option == "1") AddCustomerUI();
                else if (option == "2") UpdateCustomerUI();
                else if (option == "3") DeleteCustomerUI();
                else if (option == "4") ViewAllCustomers();
                else if (option == "5") AdvanceSearchUI();
                else ConsoleUtiles.PauseForKeyPress("Invalid Input!");
            }
        } 
        private string CustomerMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("----------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n                CUSTOMER MANGEMENT                \n\n" +

                              "----------------------------------------------------\n"
            );
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("1. "); Console.ForegroundColor = ConsoleColor.Black; Console.WriteLine("Add New Customer\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("2. "); Console.ForegroundColor = ConsoleColor.Black; Console.WriteLine("Update Customer\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("3. "); Console.ForegroundColor = ConsoleColor.Black; Console.WriteLine("Delete Customer\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("4. "); Console.ForegroundColor = ConsoleColor.Black; Console.WriteLine("View All Customers\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("5. "); Console.ForegroundColor = ConsoleColor.Black; Console.WriteLine("Advance Search\n");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("0. Go Back to Main Menu\n\n" +
                              "----------------------------------------------------\n"
            );
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("Enter your choice: ");

            return Console.ReadLine();
        }
        
        public void AddCustomerUI()
        {
            while (true)
            {
                Console.Clear();
                AddCustomerHeader();

                // Name
                string name = ConsoleUtiles.GetInput("Enter Customer's Name: ", "string");
                if (name == "exit") break;
                if (service.Exists(name))
                {
                    ConsoleUtiles.PauseForKeyPress("Customer Already Exists.");
                    continue;
                }

                // Phone Number
                string phoneNumber;
                while (true)
                {
                    phoneNumber = ConsoleUtiles.GetInput("\nEnter Customer's Phone Number: ", "string");
                    if (phoneNumber == "exit") return;
                    if (!OtherUtils.IsAllDigits(phoneNumber))
                    {
                        Console.WriteLine("\nInvalid Input!");
                        continue;
                    }
                    break;
                }

                // Age
                string ageStr = ConsoleUtiles.GetInput("\nEnter Customer's age: ", "int");
                if (ageStr == "exit") return;
                int age = int.Parse(ageStr);        

                // Address
                string address = ConsoleUtiles.GetInput("\nEnter Customer's address: ", "string");
                if (address == "exit") return; 

                service.Add(new CustomerModel(name, phoneNumber, age, address));
                break;
            }
        }
        private void AddCustomerHeader()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("----------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n                 ADD NEW CUSTOMER                 \n\n" +

                              "----------------------------------------------------\n\n" +

                              "You can type \"exit\" to go back anytime.\n"
            );
            Console.ForegroundColor = ConsoleColor.Black;
        }

        private void UpdateCustomerUI()
        {
            Console.Clear();
            UpdateCustomerHeader();

            while (true)
            {
                string name = ConsoleUtiles.GetInput("Enter Customer's Name: ", "string");
                if (name == "exit") break;
                if (!service.Exists(name))
                {
                    ConsoleUtiles.PauseForKeyPress("Customer Not Found.");
                    continue;
                }
                else
                {
                    StartUpdateLoop(service.GetCustomerByName(name));
                    break;
                }
            }
        }
        private void StartUpdateLoop(CustomerModel customer)
        {
            CustomerModel updated = new CustomerModel(customer);
            string lastOption = "Cancel";
            while (true)
            {
                Console.Clear();
                string option = UpdateMenu(updated, lastOption);

                if (option.ToLower() == "exit" || option == "0") break;
                else if (option == "1")
                {
                    string phoneNumber;
                    while (true)
                    {
                        phoneNumber = ConsoleUtiles.GetInput("\nEnter New Phone Number: ", "string");
                        if (phoneNumber == "exit") return;
                        if (!OtherUtils.IsAllDigits(phoneNumber))
                        {
                            Console.WriteLine("\nInvalid Input!");
                            continue;
                        }
                        break;
                    }
                    updated.SetPhoneNumber(phoneNumber);
                    service.Update(updated);
                    lastOption = "Done? Go Back";
                }
                else if (option == "2")
                {
                    int age;
                    while (true)
                    {
                        Console.Write("\nEnter new age: ");
                        string ageStr = Console.ReadLine();
                        if (ageStr.ToLower() == "exit") return;
                        if (ageStr == "") continue;
                        if (!int.TryParse(ageStr, out age))
                        {
                            Console.WriteLine("\nInvalid Input");
                            continue;
                        }
                        break;
                    }
                    updated.SetAge(age);
                    service.Update(updated);
                    lastOption = "Done? Go Back";
                }
                else if (option == "3")
                {
                    string address;
                    while (true)
                    {
                        Console.Write("\nEnter new address: ");
                        address = Console.ReadLine();
                        if (address.ToLower() == "exit") return;
                        if (address == "") continue;
                        break;
                    }
                    updated.SetAddress(address);
                    service.Update(updated);
                    lastOption = "Done? Go Back";
                }
                else
                {
                    ConsoleUtiles.PauseForKeyPress("Invalid Choice!");
                }
            }
        }
        private string UpdateMenu(CustomerModel customer, string lastOption)
        {
            UpdateCustomerHeader();
            Console.WriteLine("What do you want to update?\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("1. "); Console.ForegroundColor = ConsoleColor.Black; Console.WriteLine("Customer's Phone Number (Current: " + customer.GetPhoneNumber() + ")\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("2. "); Console.ForegroundColor = ConsoleColor.Black; Console.WriteLine("Customer's Age (Current: " + customer.GetAge() + ")\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("3. "); Console.ForegroundColor = ConsoleColor.Black; Console.WriteLine("Customer's Address (Current: " + customer.GetAddress() + ")\n");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("0. " + lastOption + "\n\n" +
                              "----------------------------------------------------\n"
            );
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("Enter your choice: ");
            return Console.ReadLine();
        }
        private void UpdateCustomerHeader()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("---------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n                 UPDATE CUSTOMER                 \n\n" +

                              "---------------------------------------------------\n\n" +

                                "You can type \"exit\" to go back anytime.\n"
            );
            Console.ForegroundColor = ConsoleColor.Black;
        }

        private void DeleteCustomerUI()
        {
            string name;
            while (true)
            {
                Console.Clear();
                DeleteCustomerHeader();
                name = ConsoleUtiles.GetInput("Enter Customer's Name: ", "string");
                if (name == "exit") break;
                if (!service.Exists(name))
                {
                    ConsoleUtiles.PauseForKeyPress("Customer Not Found.");
                    continue;
                }
                break;
            }
            service.Delete(name);
            ConsoleUtiles.PauseForKeyPress("Customer Deleted.");
        }
        private void DeleteCustomerHeader()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("---------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n                 DELETE CUSTOMER                 \n\n" +

                              "---------------------------------------------------\n\n" +

                              "You can type \"exit\" to go back anytime.\n"
            );
            Console.ForegroundColor = ConsoleColor.Black;
        }

        private void ViewAllCustomers()
        {
            Console.Clear();
            ViewAllCustomersHeader();
            List<CustomerModel> customers = service.GetAll();
            DisplayCustomers(customers);
        }
        private void ViewAllCustomersHeader()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("----------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n                VIEW ALL CUSTOMERS                \n\n" +

                              "----------------------------------------------------\n"
            );
            Console.ForegroundColor = ConsoleColor.Black;
        }

        private void AdvanceSearchUI()
        {  
            while (true)
            {
                Console.Clear();
                AdvanceSearchHeader();
                string option = AdvanceSearchMenu();
                if (option == "0") break;
                else if (option == "1") SearchCustomerByNameUI();
                else if (option == "2") SearchCustomersByFirstCharUI();
                else if (option == "3") SearchCustomerByPhoneNumberUI();
                else if (option == "4") SearchCustomersByAddressUI();
                else if (option == "5") SearchCustomersByAgeUI();
                else ConsoleUtiles.PauseForKeyPress("Invalid Input.");
            }

        }
        private void SearchCustomerByNameUI()
        {
            Console.Clear();
            AdvanceSearchHeader();
            string name = ConsoleUtiles.GetInput("\nEnter Customer's Name: ", "string");
            if (name == "exit") return;
            CustomerModel customer = service.GetCustomerByName(name);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Name: " + customer.GetName());
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\t" + customer.GetInfo() + "\n");
            ConsoleUtiles.PauseForKeyPress("");
        }
        private void SearchCustomersByFirstCharUI()
        {
            Console.Clear();
            AdvanceSearchHeader();
            string firstChar = ConsoleUtiles.GetInput("\nEnter First Character: ", "char");
            if (firstChar == "exit") return;
            DisplayCustomers(service.GetCustomerByFirstChar(firstChar));
        }
        private void SearchCustomerByPhoneNumberUI()
        {
            Console.Clear();
            AdvanceSearchHeader();
            string phoneNumber = ConsoleUtiles.GetInput("\nEnter Phone Number: ", "string");
            if (phoneNumber == "exit") return;
            if (!OtherUtils.IsAllDigits(phoneNumber))
            {
                ConsoleUtiles.PauseForKeyPress("Invalid Input.");
                return;
            }
            CustomerModel customer = service.GetCustomerByPhoneNumber(phoneNumber);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Name: " + customer.GetName());
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\t" + customer.GetInfo() + "\n");
            ConsoleUtiles.PauseForKeyPress("");
        }
        private void SearchCustomersByAddressUI()
        {
            Console.Clear();
            AdvanceSearchHeader();
            string address = ConsoleUtiles.GetInput("\nEnter address: ", "string");
            if (address == "exit") return;
            DisplayCustomers(service.GetCustomerByAddress(address));
        }
        private void SearchCustomersByAgeUI()
        {
            Console.Clear();
            AdvanceSearchHeader();
            string age = ConsoleUtiles.GetInput("\nEnter age: ", "int");
            if (age == "exit") return;
            DisplayCustomers(service.GetCustomerByAge(int.Parse(age)));
        }
        private string AdvanceSearchMenu()
        {
            Console.Write("1. "); Console.ForegroundColor = ConsoleColor.Black; Console.WriteLine("Find Customer by Name\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("2. "); Console.ForegroundColor = ConsoleColor.Black; Console.WriteLine("Find Customer by First Character\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("3. "); Console.ForegroundColor = ConsoleColor.Black; Console.WriteLine("Find Customer by Phone Number\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("4. "); Console.ForegroundColor = ConsoleColor.Black; Console.WriteLine("Find Customer by Address\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("5. "); Console.ForegroundColor = ConsoleColor.Black; Console.WriteLine("Find Customer by Age\n");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("0. Go Back\n\n"  +
                              "----------------------------------------------------\n"
            );
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("Enter your choice: ");
            return Console.ReadLine();
        }
        private void AdvanceSearchHeader()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("----------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n                 ADVANCE SEARCH                   \n\n" +

                              "----------------------------------------------------\n"
            );
            Console.ForegroundColor = ConsoleColor.Black;
        }

        private void DisplayCustomers(List<CustomerModel> customers)
        {
            for (int i = 0; i < customers.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(i + 1 + ". Name: " + customers[i].GetName());
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("\t" + customers[i].GetInfo() + "\n");
            }
            ConsoleUtiles.PauseForKeyPress("");
        }
    }
}
