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

        public bool Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection))
            {
                conn.Open();
                string query = "DELETE FROM Products WHERE id = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0) return true;
                return false;
            }
        }

        public bool Update(ProductModel product)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection))
            {
                Console.Write(product.ToString());
                conn.Open();
                string query = "UPDATE Products SET name=@name, purchasePrice=@purchasePrice, discount=@discount WHERE id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", product.id);
                cmd.Parameters.AddWithValue("@name", product.name);
                cmd.Parameters.AddWithValue("@purchasePrice", product.purchasePrice);
                cmd.Parameters.AddWithValue("@discount", product.discount);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0) return true;
                return false;
            }
        }

        public bool Exists(int id)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection))
            {
                conn.Open();
                string query = "SELECT COUNT(1) FROM Products WHERE id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        public ProductModel GetProductById(int id)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection))
            {
                conn.Open();
                string query = "SELECT * FROM Products WHERE id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string name = reader["name"].ToString();
                    double purchasePrice = Convert.ToDouble(reader["purchasePrice"]);
                    double discount = (reader["discount"] == DBNull.Value) ? 0.0 : Convert.ToDouble(reader["discount"]);
                    return new ProductModel(id, name, purchasePrice, discount);
                }
                return null;
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

        public List<ProductModel> GetByName(string name)
        {
            List<ProductModel> products = new List<ProductModel>();
            using (SqlConnection conn = new SqlConnection(DBConnection))
            {
                conn.Open();
                string query = "SELECT * FROM Products WHERE name = @name";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", name);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id"]);
                    double purchasePrice = Convert.ToDouble(reader["purchasePrice"]);
                    double discount = Convert.ToDouble(reader["discount"]);
                    products.Add(new ProductModel(id, name, purchasePrice, discount));
                }
                reader.Close();
            }
            return products;
        }

        public List<ProductModel> GetByPrice(double price)
        {
            List<ProductModel> products = new List<ProductModel>();
            using (SqlConnection conn = new SqlConnection(DBConnection))
            {
                conn.Open();
                string query = "SELECT * FROM Products WHERE purchasePrice = @price";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@price", price);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id"]);
                    string name = reader["name"].ToString();
                    double discount = Convert.ToDouble(reader["discount"]);
                    products.Add(new ProductModel(id, name, price, discount));
                }
                reader.Close();
            }
            return products;
        }

        public List<ProductModel> GetByPriceRange(double from, double to)
        {
            List<ProductModel> products = new List<ProductModel>();
            using (SqlConnection conn = new SqlConnection(DBConnection))
            {
                conn.Open();
                string query = "SELECT * FROM Products WHERE purchasePrice >= @from AND purchasePrice <= @to";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@from", from);
                cmd.Parameters.AddWithValue("@to", to);

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

        public List<ProductModel> GetByPriceDifference(double difference)
        {
            List<ProductModel> products = new List<ProductModel>();
            using (SqlConnection conn = new SqlConnection(DBConnection))
            {
                conn.Open();
                string query = "SELECT * FROM Products WHERE purchasePrice - (purchasePrice - (purchasePrice * discount / 100)) = @difference";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@difference", difference);
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

        public List<ProductModel> GetBySubString(string subString)
        {
            List<ProductModel> products = new List<ProductModel>();
            using (SqlConnection conn = new SqlConnection(DBConnection))
            {
                conn.Open();
                string query = "SELECT * FROM Products WHERE name LIKE @ss";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ss", $"%[{subString}]%");

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