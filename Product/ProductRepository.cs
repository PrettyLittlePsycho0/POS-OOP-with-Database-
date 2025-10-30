using System.Reflection.Metadata.Ecma335;
using ShopManagementSystem.Common;
using ShopManagementSystem.Product;

namespace ShopManagementSystem.Product
{
    internal class ProductRepository
    {
        private readonly string file = "Products.txt";
        public void Add(ProductModel product)
        {
            string filePath = FileUtils.GetPath(file);
            if (!File.Exists(filePath)) File.Create(filePath).Close();
            bool isEmpty = FileUtils.IsFileEmpty(filePath);

            using (StreamWriter file = new StreamWriter(filePath, true))
            {
                if (!isEmpty) file.WriteLine();
                file.Write(product.ToString());
            }
        }

        public void SaveAll(List<ProductModel> products)
        {
            string filePath = FileUtils.GetPath(file);
            if (!File.Exists(filePath)) File.Create(filePath).Close();

            using (StreamWriter file = new StreamWriter(filePath))
            {
                for (int i = 0; i < products.Count; i++)
                {
                    if (i != 0) file.WriteLine();
                    file.Write(products[i].ToString());
                }
            }
        }

        public List<ProductModel> GetAll()
        {
            string filePath = FileUtils.GetPath(file);
            if (!File.Exists(filePath)) File.Create(filePath).Close();
            List<ProductModel> products = new List<ProductModel>();

            foreach (string record in File.ReadLines(filePath))
            {
                if (!string.IsNullOrWhiteSpace(record))
                {
                    ProductModel temp = ParseProductRecord(record);
                    if (temp != null)
                    {
                        products.Add(temp);
                    }
                }
            }

            return products; 
        }

        private ProductModel ParseProductRecord(string record)
        {
            string name = FileUtils.ParseRecord(record, 1, '~');
            if (name == "") return null;
            if (!double.TryParse(FileUtils.ParseRecord(record, 2, '~'), out double purchasePrice)) return null;
            if (!double.TryParse(FileUtils.ParseRecord(record, 3, '~'), out double discount)) return null;
            return new ProductModel(name, purchasePrice, discount);
        }
    }
}