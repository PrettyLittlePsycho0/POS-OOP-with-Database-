using ShopManagementSystem.Common;
using ShopManagementSystem.Customer;
using ShopManagementSystem.Product;

namespace ShopManagementSystem.Order
{
    internal class OrderRepository
    {
        private readonly string file = "Orders.txt";
        private readonly List<CustomerModel> customers;

        public OrderRepository(List<CustomerModel> customers)
        {
            this.customers = customers;
        }
        public void Add(OrderModel order)
        {
            string filePath = FileUtils.GetPath(file);
            if (!File.Exists(filePath)) File.Create(filePath).Close();
            bool isEmpty = FileUtils.IsFileEmpty(filePath);

            using (StreamWriter file = new StreamWriter(filePath, true))
            {
                if (!isEmpty) file.WriteLine();
                file.Write(order.ToString());
            }
        }

        public void SaveAll(List<OrderModel> orders)
        {
            string filePath = FileUtils.GetPath(file);
            if (!File.Exists(filePath)) File.Create(filePath).Close();

            using (StreamWriter file = new StreamWriter(filePath))
            {
                for (int i = 0; i < orders.Count; i++)
                {
                    if (i != 0) file.WriteLine();
                    file.Write(orders[i].ToString());
                }
            }
        }

        public List<OrderModel> GetAll()
        {
            string filePath = FileUtils.GetPath(file);
            if (!File.Exists(filePath)) File.Create(filePath).Close();
            List<OrderModel> orders = new List<OrderModel>();

            foreach (string record in File.ReadLines(filePath))
            {
                if (!string.IsNullOrWhiteSpace(record))
                {
                    OrderModel temp = ParseOrderRecord(record, customers);
                    if (temp != null)
                    {
                        orders.Add(temp);
                    }
                }
            }

            return orders;
        }

        private OrderModel ParseOrderRecord(string record, List<CustomerModel> customers)
        {
            List<OrderItem> items = new List<OrderItem>();
            string customerName = FileUtils.ParseRecord(record, 1, '~');
            if (customerName == "") return null;

            int numberOfItems = int.Parse(FileUtils.ParseRecord(record, 2, '~'));
            string itemsRecord = FileUtils.ParseRecord(record, 3, '~');
            for (int i = 1; i <= numberOfItems * 4; i += 4)
            {
                items.Add(
                    new OrderItem(
                        new ProductModel(
                            FileUtils.ParseRecord(itemsRecord, i, '`'),
                            double.Parse(FileUtils.ParseRecord(itemsRecord, i + 1, '`')),
                            double.Parse(FileUtils.ParseRecord(itemsRecord, i + 2, '`'))
                        ),
                        int.Parse(FileUtils.ParseRecord(itemsRecord, i + 3, '`'))
                    )
                );
            }

            foreach (CustomerModel c in customers)
            {
                if (c.GetName() == customerName)
                {
                    CustomerModel customer = new CustomerModel(c);
                    return new OrderModel(customer, items);
                }
            }
            return null;
        }
    }
}