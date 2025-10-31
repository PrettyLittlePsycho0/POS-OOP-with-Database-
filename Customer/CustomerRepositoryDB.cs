using Microsoft.Data.SqlClient;

namespace ShopManagementSystem.Customer
{
    internal class CustomerRepositoryDB
    {
        private readonly string DBConnection = "Server=localhost;Database=POS;Trusted_Connection=True;TrustServerCertificate=True;";


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
    }
}