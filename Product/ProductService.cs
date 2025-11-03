using ShopManagementSystem.Common;

namespace ShopManagementSystem.Product
{
    internal class ProductService
    {
        private List<ProductModel> allProducts;
        private ProductRepository repo;
        private ProductRepositoryDB repoDB;

        public ProductService()
        {
            repoDB = new ProductRepositoryDB();
            repo = new ProductRepository();
            allProducts = repo.GetAll();
        }

        public bool Create(ProductModel product)
        {
            //allProducts.Add(product);
            //repo.Add(product);
            return repoDB.Create(product);
        }

        public bool Update(ProductModel updated)
        {
            /*for (int i = 0; i < allProducts.Count; i++)
            {
                if (allProducts[i].GetName() == updated.GetName())
                {
                    allProducts[i] = new ProductModel(updated);
                    break;
                }
            }
            repo.SaveAll(allProducts);*/
            return repoDB.Update(new ProductModel(updated));
        }

        public bool Delete(int id)
        {
            /*foreach (ProductModel product in allProducts)
            {
                if (product.GetName() == name)
                {
                    allProducts.Remove(product);
                    repo.SaveAll(allProducts);
                    break;
                }
            }*/
            return repoDB.Delete(id);
        }

        public List<ProductModel> GetAll()
        {
            //return allProducts;
            return repoDB.GetAll();
        }

        public ProductModel GetProductById(int id)
        {
            return repoDB.GetCustomerById(id);
        }

        public ProductModel GetProductByName(string name)
        {
            foreach (ProductModel product in allProducts)
            {
                if (product.GetName() == name) return product;
            }
            return null;
        }

        public List<ProductModel> GetProductsByPrice(double price)
        {
            List<ProductModel> products = new List<ProductModel>();
            foreach (ProductModel product in allProducts)
            {
                if (product.GetPurchasePrice() == price) products.Add(product);
            }
            return products;
        }
        public List<ProductModel> GetProductsByPriceRange(double from, double to)
        {
            List<ProductModel> products = new List<ProductModel>();
            foreach (ProductModel product in allProducts)
            {
                if (product.GetPurchasePrice() >= from && product.GetPurchasePrice() <= to) products.Add(product);
            }
            return products;
        }
        public List<ProductModel> GetProductsByPriceDifference(double difference)
        {
            List<ProductModel> products = new List<ProductModel>();
            foreach (ProductModel product in allProducts)
            {
                if (product.GetPurchasePrice() - product.CalculateSalePrice() == difference) products.Add(product);
            }
            return products;
        }
        public List<ProductModel> GetProductsBySubString(string subString)
        {
            List<ProductModel> products = new List<ProductModel>();
            foreach (ProductModel product in allProducts)
            {
                foreach (char c in subString.ToLower())
                {
                    if (product.GetName().ToLower().Contains(c))
                    {
                        products.Add(product);
                        break;
                    }
                }
            }
            return products;
        }
        public bool Exists(int id)
        {
            /*foreach (ProductModel product in allProducts)
            {
                if (product.GetName() == name) return true;
            }
            return false;*/
            return repoDB.Exists(id);
        }
    }
} 