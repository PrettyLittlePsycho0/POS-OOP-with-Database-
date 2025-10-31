using ShopManagementSystem.Common;
using Microsoft.Data.SqlClient;

namespace ShopManagementSystem.Product
{
    internal class ProductRepositoryDB
    {
        private readonly string DBConnection = OtherUtils.GetConnectionString();

        public bool Create(ProductModel product)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection))
            {
                string query = "INSERT INTO Products (name, purchasePrice, discount)" +
                               "VALUES (@name, @purchasePrice, @discount)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", product.name);
                cmd.Parameters.AddWithValue("@purchasePrice", product.purchasePrice);
                cmd.Parameters.AddWithValue("@discount", product.discount);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0) return true;
                return false;
            }
        }

        public List<ProductModel> GetAll()
        {
            List<ProductModel> products = new List<ProductModel>();
            using (SqlConnection conn = new SqlConnection(DBConnection))
            {
                conn.Open();
                string query = "SELECT * FROM Products";
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id"]);
                    string name = reader["name"].ToString();
                    double purchasePrice = Convert.ToDouble(reader["purchasePrice"]);
                    double discount = Convert.ToDouble(reader["discount"]);
                    products.Add(new ProductModel(id, name, purchasePrice, discount));
                }
                reader.Close();
            }
            return products;
        }
    }
}