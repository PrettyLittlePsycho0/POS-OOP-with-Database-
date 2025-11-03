using ShopManagementSystem.Customer;

namespace ShopManagementSystem.Order
{
    internal class OrderService
    {
        private List<OrderModel> allOrders;
        private OrderRepository repo;
        private OrderRepositoryDB repoDB;
        private CustomerService customerService;

        public OrderService()
        {
            repoDB = new OrderRepositoryDB();
            customerService = new CustomerService();
            repo = new OrderRepository(customerService.GetAll());
            allOrders = repo.GetAll();
        }

        public bool Create(OrderModel order)
        {
            //allOrders.Add(new OrderModel(order));
            //repo.Add(order);
            return repoDB.Create(order);
        }

        public List<OrderModel> GetAll()
        {
            return repoDB.GetAll();
            //return repo.GetAll();
        }
        public List<OrderModel> GetOrdersByCustomerId(int customerId)
        {
            return repoDB.GetOrdersByCustomerId(customerId);
        }
    }
}