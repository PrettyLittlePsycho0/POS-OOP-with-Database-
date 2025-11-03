using System.Security.Cryptography.Pkcs;
using System.Data;
using Microsoft.Data.SqlClient;
using ShopManagementSystem.Common;
using ShopManagementSystem.Customer;
using ShopManagementSystem.Product;

namespace ShopManagementSystem.Order
{
    internal class OrderRepositoryDB
    {
        private readonly string DBConnection = OtherUtils.GetConnectionString();

        public bool Create(OrderModel order)
        {
            using (SqlConnection conn = new SqlConnection(DBConnection))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();
                try
                {
                    string insertOrderQuery = "INSERT INTO Orders (customerId, customerName, orderDate) " +
                                            "VALUES (@customerId, @customerName, @orderDate); " +
                                            "SELECT SCOPE_IDENTITY();";
                    SqlCommand cmd = new SqlCommand(insertOrderQuery, conn, transaction);
                    cmd.Parameters.AddWithValue("@customerId", order.customer.id);
                    cmd.Parameters.AddWithValue("@customerName", order.customer.name);
                    cmd.Parameters.Add("@orderDate", SqlDbType.DateTime).Value = order.dateTime;

                    object result = cmd.ExecuteScalar();
                    int newOrderId = 0;

                    if (result != null && result != DBNull.Value)
                    {
                        newOrderId = Convert.ToInt32(result);
                        order.id = newOrderId;
                    }
                    else
                    {
                        throw new InvalidOperationException("Failed to retrieve the new Order ID.");
                    }

                    string insertItemQuery = "INSERT INTO OrderItems (orderId, productId, productName, quantity, purchasePrice, discount) " +
                                            "VALUES (@orderId, @productId, @productName, @quantity, @purchasePrice, @discount)";

                    foreach (OrderItem item in order.items)
                    {
                        SqlCommand cmd2 = new SqlCommand(insertItemQuery, conn, transaction);
                        cmd2.Parameters.AddWithValue("@orderId", order.id);

                        cmd2.Parameters.AddWithValue("@productId", item.product.id);
                        cmd2.Parameters.AddWithValue("@productName", item.product.name);
                        cmd2.Parameters.AddWithValue("@quantity", item.quantity);
                        cmd2.Parameters.AddWithValue("@purchasePrice", item.product.purchasePrice);
                        cmd2.Parameters.AddWithValue("@discount", item.product.discount);

                        int rowsAffected = cmd2.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            throw new InvalidOperationException("Order item insertion failed for a product.");
                        }
                    }
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Database Transaction Failed: {ex.Message}");
                    return false;
                }
            }
        }

        public List<OrderModel> GetAll()
        {
            List<OrderModel> orders = new List<OrderModel>();
            using (SqlConnection conn = new SqlConnection(DBConnection))
            {
                conn.Open();
                string query = "SELECT * FROM Orders";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id"]);
                    int customerId = Convert.ToInt32(reader["customerId"]);
                    string customerName = reader["customerName"].ToString();
                    DateTime dateTime = Convert.ToDateTime(reader["orderDate"]);
                    if (new CustomerService().Exists(customerId))
                    {
                        CustomerModel customer = new CustomerModel(customerId, customerName);
                        orders.Add(new OrderModel(id, customer, dateTime, new List<OrderItem>()));
                    }
                    else
                    {
                        orders.Add(new OrderModel(id, customerId, customerName, dateTime, new List<OrderItem>()));
                    }

                }
                reader.Close();
                foreach (OrderModel order in orders)
                {
                    string query2 = "SELECT * FROM OrderItems WHERE orderId=@orderId";
                    SqlCommand cmd2 = new SqlCommand(query2, conn);
                    cmd2.Parameters.AddWithValue("@orderId", order.id);
                    SqlDataReader reader2 = cmd2.ExecuteReader();
                    while (reader2.Read())
                    {
                        int productId = Convert.ToInt32(reader2["productId"]);
                        string productName = reader2["productName"].ToString();
                        double purchasePrice = Convert.ToDouble(reader2["purchasePrice"]);
                        double discount = Convert.ToDouble(reader2["discount"]);
                        ProductModel product = new ProductModel(productId, productName, purchasePrice, discount);
                        int quantity = Convert.ToInt32(reader2["quantity"]);
                        order.items.Add(new OrderItem(product, quantity));
                    }
                    reader2.Close();
                }
            }
            return orders;
        }
        public List<OrderModel> GetOrdersByCustomerId(int customerId)
        {
            List<OrderModel> orders = new List<OrderModel>();
            using (SqlConnection conn = new SqlConnection(DBConnection))
            {
                conn.Open();
                string query = "SELECT * FROM Orders WHERE customerId = @customerId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@customerId", customerId);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id"]);
                    string customerName = reader["customerName"].ToString();
                    DateTime dateTime = Convert.ToDateTime(reader["orderDate"]);
                    if (new CustomerService().Exists(customerId))
                    {
                        CustomerModel customer = new CustomerModel(customerId, customerName);
                        orders.Add(new OrderModel(id, customer, dateTime, new List<OrderItem>()));
                    }
                    else
                    {
                        orders.Add(new OrderModel(id, customerId, customerName, dateTime, new List<OrderItem>()));
                    }

                }
                reader.Close();
                foreach (OrderModel order in orders)
                {
                    string query2 = "SELECT * FROM OrderItems WHERE orderId=@orderId";
                    SqlCommand cmd2 = new SqlCommand(query2, conn);
                    cmd2.Parameters.AddWithValue("@orderId", order.id);
                    SqlDataReader reader2 = cmd2.ExecuteReader();
                    while (reader2.Read())
                    {
                        int productId = Convert.ToInt32(reader2["productId"]);
                        string productName = reader2["productName"].ToString();
                        double purchasePrice = Convert.ToDouble(reader2["purchasePrice"]);
                        double discount = Convert.ToDouble(reader2["discount"]);
                        ProductModel product = new ProductModel(productId, productName, purchasePrice, discount);
                        int quantity = Convert.ToInt32(reader2["quantity"]);
                        order.items.Add(new OrderItem(product, quantity));
                    }
                    reader2.Close();
                }
            }
            return orders;
        }
    }
}