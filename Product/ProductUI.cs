using System;
using ShopManagementSystem.Common;

namespace ShopManagementSystem.Product
{
    internal class ProductUI
    {
        private ProductService service;

        public ProductUI()
        {
            service = new ProductService();
        }

        public void StartLoop()
        {
            while (true)
            {
                string option = ProductMenu();
                if (option == "0") break;
                else if (option == "1") AddProductUI();
                else if (option == "2") UpdateProductUI();
                else if (option == "3") DeleteProductUI();
                else if (option == "4") ViewAllProducts();
                else if (option == "5") AdvanceSearchUI();
                else ConsoleUtiles.PauseForKeyPress("Invalid Input!");
            }
        }
        public string ProductMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("---------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n                PRODUCT MANGEMENT                \n\n" +

                              "---------------------------------------------------\n"
            );
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("1. "); Console.ForegroundColor = ConsoleColor.Black; Console.WriteLine("Add New Product\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("2. "); Console.ForegroundColor = ConsoleColor.Black; Console.WriteLine("Update Product\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("3. "); Console.ForegroundColor = ConsoleColor.Black; Console.WriteLine("Delete Product\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("4. "); Console.ForegroundColor = ConsoleColor.Black; Console.WriteLine("View All Products\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("5. "); Console.ForegroundColor = ConsoleColor.Black; Console.WriteLine("Advance Search\n");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("0. Go Back to Main Menu\n\n" +
                              "---------------------------------------------------\n"
            );
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("Enter your choice: ");

            return Console.ReadLine();
        }

        private void AddProductUI()
        {
            Console.Clear();
            AddProductHeader();

            // Name
            string name = ConsoleUtiles.GetInput("Enter Product's Name: ", "string");
            if (name == "exit") return;
            

            // Purchase Price
            double purchasePrice;
            while (true)
            {
                string purchasePriceStr = ConsoleUtiles.GetInput("\nEnter Product's purchase price: ", "double");
                if (purchasePriceStr == "exit") return;
                purchasePrice = double.Parse(purchasePriceStr);
                break;
            }

            // Discount
            double discountPercentage = 0;
            while (true)
            {
                string choice = ConsoleUtiles.GetInput("\nDo you want to add a discount for this product? (Yes/No): ", "string");
                if (choice == "exit") break;
                if (choice.ToLower() == "no") break;
                else if (choice.ToLower() == "yes")
                {
                    while (true)
                    {
                        string discountPercentageStr = ConsoleUtiles.GetInput("\nEnter discount percentage: ", "double");
                        if (discountPercentageStr == "exit") return;
                        discountPercentage = double.Parse(discountPercentageStr);
                        if (discountPercentage > 100)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nInvalid percentage.");
                            Console.ForegroundColor = ConsoleColor.Black;
                            continue;
                        }
                        break;
                    }
                }
                break;
            }
            if (service.Create(new ProductModel(name, purchasePrice, discountPercentage)))
            {
                ConsoleUtiles.PauseForKeyPress("Product Added Successfully.");
            }
            else
            {
                ConsoleUtiles.PauseForKeyPress("Error creating the product.");
            }
        }
        private void AddProductHeader()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("----------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n                 ADD NEW PRODUCT                  \n\n" +

                              "----------------------------------------------------\n\n" +

                              "You can type \"exit\" to go back anytime.\n"
            );
            Console.ForegroundColor = ConsoleColor.Black;
        }

        private void UpdateProductUI()
        {
            Console.Clear();
            UpdateProductHeader();

            string id = ConsoleUtiles.GetInput("\nEnter Product's ID: ", "int");
            if (id == "exit") return;
            else
            {
                StartUpdateLoop(service.GetProductById(int.Parse(id)));
            }
        }
        private void StartUpdateLoop(ProductModel product)
        {
            ProductModel updated = new ProductModel(product);
            string lastOption = "Cancel";
            while (true)
            {
                Console.Clear();
                string option = UpdateMenu(updated, lastOption);

                if (option.ToLower() == "exit" || option == "0") break;
                else if (option == "1")
                {
                    string name = ConsoleUtiles.GetInput("\nEnter New Name: ", "string");
                    if (name == "exit") return;

                    updated.name = name;
                    if (service.Update(updated))
                    {
                        lastOption = "Done? Go Back";
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nThere was an error updating the attribute.");
                        Console.ForegroundColor = ConsoleColor.Black;
                        continue;
                    }
                }
                else if (option == "2")
                {
                    string purchasePriceStr = ConsoleUtiles.GetInput("\nEnter new purchase price: ", "double");
                    if (purchasePriceStr == "exit") return;
                    double purchasePrice = double.Parse(purchasePriceStr);

                    updated.purchasePrice = purchasePrice;
                    if (service.Update(updated))
                    {
                        lastOption = "Done? Go Back";
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nThere was an error updating the attribute.");
                        Console.ForegroundColor = ConsoleColor.Black;
                        continue;
                    }
                }
                else if (option == "3")
                {
                    double discountPercentage;
                    while (true)
                    {
                        string discountPercentageStr = ConsoleUtiles.GetInput("\nEnter new discount percentage: ", "double");
                        if (discountPercentageStr == "exit") return;
                        discountPercentage = double.Parse(discountPercentageStr);
                        if (discountPercentage > 100)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nInvalid percentage.");
                            Console.ForegroundColor = ConsoleColor.Black;
                            continue;
                        }
                        break;
                    }
                    updated.discount = discountPercentage;
                    if (service.Update(updated))
                    {
                        lastOption = "Done? Go Back";
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nThere was an error updating the attribute.");
                        Console.ForegroundColor = ConsoleColor.Black;
                        continue;
                    }
                }
                else
                {
                    ConsoleUtiles.PauseForKeyPress("Invalid Choice!");
                }
            }
        }
        private string UpdateMenu(ProductModel product, string lastOption)
        {
            UpdateProductHeader();
            Console.WriteLine("What do you want to update?\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("1. "); Console.ForegroundColor = ConsoleColor.Black; Console.WriteLine("Product Name (Current: " + product.name + ")\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("2. "); Console.ForegroundColor = ConsoleColor.Black; Console.WriteLine("Product Price (Current: $" + product.purchasePrice + ")\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("3. "); Console.ForegroundColor = ConsoleColor.Black; Console.WriteLine("Product Discount (Current: " + product.discount + ")\n");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("0. " + lastOption + "\n\n" +
                              "----------------------------------------------------\n"
            );
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("Enter your choice: ");
            return Console.ReadLine();
        }
        private void UpdateProductHeader()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("---------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n                 UPDATE PRODUCT                  \n\n" +

                              "---------------------------------------------------\n\n" +

                                "You can type \"exit\" to go back anytime.\n"
            );
            Console.ForegroundColor = ConsoleColor.Black;
        }

        private void DeleteProductUI()
        {
            Console.Clear();
            DeleteProductHeader();
            string id = ConsoleUtiles.GetInput("\nEnter Product ID: ", "int");
            if (id == "exit") return;
            if (!service.Exists(int.Parse(id)))
            {
                ConsoleUtiles.PauseForKeyPress("Product Not Found.");
            }
            if (service.Delete(int.Parse(id)))
            {
                ConsoleUtiles.PauseForKeyPress("Product Deleted Successfully.");
            }
            else
            {
                ConsoleUtiles.PauseForKeyPress("Error Creating the Product.");
            }
        }
        private void DeleteProductHeader()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("---------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n                 DELETE PRODUCT                  \n\n" +

                              "---------------------------------------------------\n\n" +

                              "You can type \"exit\" to go back anytime.\n"
            );
            Console.ForegroundColor = ConsoleColor.Black;
        }

        private void ViewAllProducts()
        {
            Console.Clear();
            ViewAllProductsHeader();
            List<ProductModel> products = service.GetAll();
            if (products.Count == 0)
            {
                ConsoleUtiles.PauseForKeyPress("There are no products in the system yet.");
            }
            else
            {
                DisplayProducts(products);
            } 
        }
        private void ViewAllProductsHeader()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("----------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n                VIEW ALL PRODUCTS                 \n\n" +

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
                else if (option == "1") SearchProductByNameUI();
                else if (option == "2") SearchProductByPriceUI();
                else if (option == "3") SearchProductByPriceRangeUI();
                else if (option == "4") SearchProductByPriceDifferenceUI();
                else if (option == "5") SearchProductBySubStringUI();
                else ConsoleUtiles.PauseForKeyPress("Invalid Input.");
            }

        }
        private void SearchProductByNameUI()
        {
            Console.Clear();
            AdvanceSearchHeader();
            string name = ConsoleUtiles.GetInput("\nEnter Product's Name: ", "string");
            if (name == "exit") return;
            ProductModel product = service.GetProductByName(name);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Name: " + product.GetName());
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\t" + product.GetInfo() + "\n");
            ConsoleUtiles.PauseForKeyPress("");
        }
        private void SearchProductByPriceUI()
        {
            Console.Clear();
            AdvanceSearchHeader();
            string price = ConsoleUtiles.GetInput("\nEnter Price: ", "double");
            if (price == "exit") return;
            DisplayProducts(service.GetProductsByPrice(double.Parse(price)));
        }
        private void SearchProductByPriceRangeUI()
        {
            Console.Clear();
            AdvanceSearchHeader();
            string from = ConsoleUtiles.GetInput("\nEnter Start Price: ", "double");
            if (from == "exit") return;
            string to = ConsoleUtiles.GetInput("\nEnter End Price: ", "double");
            if (to == "exit") return;
            DisplayProducts(service.GetProductsByPriceRange(double.Parse(from), double.Parse(to)));
        }
        private void SearchProductByPriceDifferenceUI()
        {
            Console.Clear();
            AdvanceSearchHeader();
            string difference = ConsoleUtiles.GetInput("\nEnter price difference between purchase and sale price: ", "double");
            if (difference == "exit") return;
            DisplayProducts(service.GetProductsByPriceDifference(double.Parse(difference)));
        }
        private void SearchProductBySubStringUI()
        {
            Console.Clear();
            AdvanceSearchHeader();
            string subString = ConsoleUtiles.GetInput("\nEnter Sub String: ", "string");
            if (subString == "exit") return;
            DisplayProducts(service.GetProductsBySubString(subString));
        }
        private string AdvanceSearchMenu()
        {
            Console.Write("1. "); Console.ForegroundColor = ConsoleColor.Black; Console.WriteLine("Search Product by Name\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("2. "); Console.ForegroundColor = ConsoleColor.Black; Console.WriteLine("Search All Products by Price\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("3. "); Console.ForegroundColor = ConsoleColor.Black; Console.WriteLine("Find Products Between a Price Range\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("4. "); Console.ForegroundColor = ConsoleColor.Black; Console.WriteLine("Find Products by Price difference\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("5. "); Console.ForegroundColor = ConsoleColor.Black; Console.WriteLine("Find Products by Substring\n");
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

        private void DisplayProducts(List<ProductModel> products)
        {
            for (int i = 0; i < products.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("ID " + products[i].id + ": Name: " + products[i].name);
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("\t" + products[i].GetInfo() + "\n");
            }
            ConsoleUtiles.PauseForKeyPress("");
        }
    } 
}