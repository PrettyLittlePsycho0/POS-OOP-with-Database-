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
            return repoDB.GetProductById(id);
        }

        public List<ProductModel> GetProductByName(string name)
        {
            return repoDB.GetByName(name);
        }

        public List<ProductModel> GetProductsByPrice(double price)
        {
            return repoDB.GetByPrice(price);
        }
        public List<ProductModel> GetProductsByPriceRange(double from, double to)
        {
            return repoDB.GetByPriceRange(from, to);
        }
        public List<ProductModel> GetProductsByPriceDifference(double difference)
        {
            return repoDB.GetByPriceDifference(difference);
        }
        public List<ProductModel> GetProductsBySubString(string subString)
        {
            return repoDB.GetBySubString(subString);
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