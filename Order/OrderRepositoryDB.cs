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
                    string insertOrderQuery = "INSERT INTO Orders (customerId, orderDate) " +
                                            "VALUES (@customerId, @orderDate); " +
                                            "SELECT SCOPE_IDENTITY();";
                    SqlCommand cmd = new SqlCommand(insertOrderQuery, conn, transaction);
                    cmd.Parameters.AddWithValue("@customerId", order.customer.id);
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

                    string insertItemQuery = "INSERT INTO OrderItems (orderId, productId, quantity, purchasePrice, discount) " +
                                            "VALUES (@orderId, @productId, @quantity, @purchasePrice, @discount)";

                    foreach (OrderItem item in order.items)
                    {
                        SqlCommand cmd2 = new SqlCommand(insertItemQuery, conn, transaction);
                        cmd2.Parameters.AddWithValue("@orderId", order.id);

                        cmd2.Parameters.AddWithValue("@productId", item.product.id);
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
                    DateTime dateTime = Convert.ToDateTime(reader["orderDate"]);
                    if (new CustomerService().Exists(customerId))
                    {
                        CustomerModel customer = new CustomerModel(new CustomerService().GetCustomerById(customerId));
                        orders.Add(new OrderModel(id, customer, dateTime, new List<OrderItem>()));
                    }
                    else
                    {
                        orders.Add(new OrderModel(id, customerId, dateTime, new List<OrderItem>()));
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
                        ProductModel product = new ProductModel(new ProductService().GetProductById(Convert.ToInt32(reader2["productId"])));
                        product.purchasePrice = Convert.ToDouble(reader2["purchasePrice"]);
                        product.discount = Convert.ToDouble(reader2["discount"]);
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