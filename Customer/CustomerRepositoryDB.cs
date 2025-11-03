using Microsoft.Data.SqlClient;
using ShopManagementSystem.Common;

namespace ShopManagementSystem.Customer
{
    internal class CustomerRepositoryDB
    {
        private readonly string DBConnection = OtherUtils.GetConnectionString();


        public bool Create(CustomerModel customer)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection))
            {
                string query = "INSERT INTO Customers (name, phoneNumber, age, address)" +
                               "VALUES (@name, @phoneNumber, @age, @address)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", customer.name);
                cmd.Parameters.AddWithValue("@phoneNumber", customer.phoneNumber);
                cmd.Parameters.AddWithValue("@age", customer.age);
                cmd.Parameters.AddWithValue("@address", customer.address);

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
                string query = "DELETE FROM Customers WHERE id = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0) return true;
                return false;
            }
        }

        public bool Update(CustomerModel customer)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection))
            {
                Console.Write(customer.ToString());
                conn.Open();
                string query = "UPDATE Customers SET name=@name, phoneNumber=@phoneNumber, age=@age, address=@address WHERE id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", customer.id);
                cmd.Parameters.AddWithValue("@name", customer.name);
                cmd.Parameters.AddWithValue("@phoneNumber", customer.phoneNumber);
                cmd.Parameters.AddWithValue("@age", customer.age);
                cmd.Parameters.AddWithValue("@address", customer.address);

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
                string query = "SELECT COUNT(1) FROM Customers WHERE id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        public CustomerModel GetCustomerById(int id)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection))
            {
                conn.Open();
                string query = "SELECT * FROM Customers WHERE id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                string name = reader["name"].ToString();
                string phoneNumber = reader["phoneNumber"].ToString();
                int age = Convert.ToInt32(reader["age"]);
                string address = reader["address"].ToString();
                return new CustomerModel(id, name, phoneNumber, age, address);
            }
        }

        public List<CustomerModel> GetAll()
        {
            List<CustomerModel> customers = new List<CustomerModel>();
            using (SqlConnection conn = new SqlConnection(DBConnection))
            {
                conn.Open();
                string query = "SELECT * FROM Customers";
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id"]);
                    string name = reader["name"].ToString();
                    string phoneNumber = reader["phoneNumber"].ToString();
                    int age = Convert.ToInt32(reader["age"]);
                    string address = reader["address"].ToString();
                    customers.Add(new CustomerModel(id, name, phoneNumber, age, address));
                }
                reader.Close();
            }
            return customers;
        }

        public List<CustomerModel> GetByName(string name)
        {
            List<CustomerModel> customers = new List<CustomerModel>();
            using (SqlConnection conn = new SqlConnection(DBConnection))
            {
                conn.Open();
                string query = "SELECT * FROM Customers WHERE name = @customerName";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@customerName", name);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id"]);
                    string phoneNumber = reader["phoneNumber"].ToString();
                    int age = Convert.ToInt32(reader["age"]);
                    string address = reader["address"].ToString();
                    customers.Add(new CustomerModel(id, name, phoneNumber, age, address));
                }
                reader.Close();
            }
            return customers;
        }
        public List<CustomerModel> GetByFirstChar(string ch)
        {
            List<CustomerModel> customers = new List<CustomerModel>();
            using (SqlConnection conn = new SqlConnection(DBConnection))
            {
                conn.Open();
                string query = "SELECT * FROM Customers WHERE name LIKE @ch";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ch", ch + "%");

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id"]);
                    string name = reader["name"].ToString();
                    string phoneNumber = reader["phoneNumber"].ToString();
                    int age = Convert.ToInt32(reader["age"]);
                    string address = reader["address"].ToString();
                    customers.Add(new CustomerModel(id, name, phoneNumber, age, address));
                }
                reader.Close();
            }
            return customers;
        }

        public CustomerModel GetByPhoneNumber(string phoneNumber)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection))
            {
                conn.Open();
                string query = "SELECT * FROM Customers WHERE phoneNumber = @phoneNumber";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@phoneNumber", phoneNumber);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id"]);
                    string name = reader["name"].ToString();
                    int age = Convert.ToInt32(reader["age"]);
                    string address = reader["address"].ToString();
                    return new CustomerModel(id, name, phoneNumber, age, address);
                }
                reader.Close();
            }
            return null;
        }

        public List<CustomerModel> GetByAddress(string address)
        {
            List<CustomerModel> customers = new List<CustomerModel>();
            using (SqlConnection conn = new SqlConnection(DBConnection))
            {
                conn.Open();
                string query = "SELECT * FROM Customers WHERE address = @address";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@address", address);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id"]);
                    string name = reader["name"].ToString();
                    string phoneNumber = reader["phoneNumber"].ToString();
                    int age = Convert.ToInt32(reader["age"]);
                    customers.Add(new CustomerModel(id, name, phoneNumber, age, address));
                }
                reader.Close();
            }
            return customers;
        }

        public List<CustomerModel> GetByAge(int age)
        {
            List<CustomerModel> customers = new List<CustomerModel>();
            using (SqlConnection conn = new SqlConnection(DBConnection))
            {
                conn.Open();
                string query = "SELECT * FROM Customers WHERE age = @age";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@age", age);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id"]);
                    string name = reader["name"].ToString();
                    string phoneNumber = reader["phoneNumber"].ToString();
                    string address = reader["address"].ToString();
                    customers.Add(new CustomerModel(id, name, phoneNumber, age, address));
                }
                reader.Close();
            }
            return customers;
        }
    }
}