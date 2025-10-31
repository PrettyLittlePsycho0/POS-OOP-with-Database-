using System.Security.Cryptography.Pkcs;
using System.Data;
using Microsoft.Data.SqlClient;
using ShopManagementSystem.Common;

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
                    string insertOrderQuery = "INSERT INTO Orders (customerId, orderDate, totalPrice) " +
                                            "VALUES (@customerId, @orderDate, @totalPrice); " +
                                            "SELECT SCOPE_IDENTITY();";
                    SqlCommand cmd = new SqlCommand(insertOrderQuery, conn, transaction);
                    cmd.Parameters.AddWithValue("@customerId", order.customer.id);
                    cmd.Parameters.Add("@orderDate", SqlDbType.DateTime).Value = order.dateTime;
                    cmd.Parameters.AddWithValue("@totalPrice", order.totalPrice);

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
                    
                    string insertItemQuery = "INSERT INTO OrderItems (orderId, productId, quantity, purchasePrice, discount, totalPrice) " +
                                            "VALUES (@orderId, @productId, @quantity, @purchasePrice, @discount, @totalPrice)";

                    foreach (OrderItem item in order.items)
                    {
                        SqlCommand cmd2 = new SqlCommand(insertItemQuery, conn, transaction);
                        cmd2.Parameters.AddWithValue("@orderId", order.id); 
                        
                        cmd2.Parameters.AddWithValue("@productId", item.product.id);
                        cmd2.Parameters.AddWithValue("@quantity", item.quantity);
                        cmd2.Parameters.AddWithValue("@purchasePrice", item.product.purchasePrice);
                        cmd2.Parameters.AddWithValue("@discount", item.product.discount);
                        cmd2.Parameters.AddWithValue("@totalPrice", item.totalPrice);

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
    }
}