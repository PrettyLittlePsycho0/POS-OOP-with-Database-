using System;
using ShopManagementSystem.Customer;

namespace ShopManagementSystem.Order
{
    internal class OrderModel
    {
        public int id { get; set; }
        public DateTime dateTime { get; private set; }
        public CustomerModel customer;
        public List<OrderItem> items;
        public double totalPrice { get; set; }

        public OrderModel(CustomerModel customer, List<OrderItem> items)
        {
            dateTime = DateTime.Now;
            this.customer = new CustomerModel(customer);
            this.items = new List<OrderItem>();
            for (int i = 0; i < items.Count(); i++)
            {
                this.items.Add(new OrderItem(items[i]));
            }
            this.totalPrice = 0;
            CalculateTotal();
        }
        public OrderModel(int id, CustomerModel customer, DateTime dateTime, List<OrderItem> items)
        {
            this.id = id;
            this.dateTime = dateTime;
            this.customer = new CustomerModel(customer);
            this.items = new List<OrderItem>();
            for (int i = 0; i < items.Count(); i++)
            {
                this.items.Add(new OrderItem(items[i]));
            }
            this.totalPrice = 0;
            CalculateTotal();
        }
        public OrderModel(int id, CustomerModel customer, List<OrderItem> items)
        {
            this.id = id;
            dateTime = DateTime.Now;
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
            dateTime = order.dateTime;
            id = order.id;
            customer = new CustomerModel(order.GetCustomer());
            items = order.GetOrderItems();
            totalPrice = 0;
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