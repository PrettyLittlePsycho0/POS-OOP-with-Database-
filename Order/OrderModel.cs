using System;
using ShopManagementSystem.Customer;

namespace ShopManagementSystem.Order
{
    internal class OrderModel
    {
        private CustomerModel customer;
        private List<OrderItem> items;
        private double totalPrice;

        public OrderModel(CustomerModel customer, List<OrderItem> items)
        {
            this.customer = new CustomerModel(customer);
            this.items = new List<OrderItem>();
            for (int i = 0; i < items.Count(); i++)
            {
                this.items.Add(new OrderItem(items[i]));
            }
            this.totalPrice = 0;
            CalculateTotal();
        }
        public OrderModel(OrderModel order)
        {
            this.customer = new CustomerModel(order.GetCustomer());
            items = order.GetOrderItems();
            this.totalPrice = 0;
            CalculateTotal();
        }

        public void CalculateTotal()
        {
            for (int i = 0; i < items.Count(); i++)
            {
                this.totalPrice += items[i].totalPrice;
            }
        }

        public override string ToString()
        {
            string record = customer.GetName() + "~" + items.Count + '~';
            foreach (var item in items)
            {
                record += item.ToString() + '`';
            }
            return record;
        }
        public double GetTotalPrice()
        {
            return totalPrice;
        }
        public List<OrderItem> GetOrderItems()
        {
            return items;
        }
        public CustomerModel GetCustomer()
        {
            return customer;
        }
    }
}