using ShopManagementSystem.Customer;

namespace ShopManagementSystem.Order
{
    internal class OrderService
    {
        private List<OrderModel> allOrders;
        private OrderRepository repo;
        private CustomerService customerService;

        public OrderService()
        {
            customerService = new CustomerService();
            repo = new OrderRepository(customerService.GetAll());
            allOrders = repo.GetAll();
        }

        public void Add(OrderModel order)
        {
            allOrders.Add(new OrderModel(order));
            repo.Add(order);
        }

        public List<OrderModel> GetAll()
        {

            return repo.GetAll();
        }
    }
}