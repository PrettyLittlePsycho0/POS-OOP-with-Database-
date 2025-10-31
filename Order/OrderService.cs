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

        public void Create(OrderModel order)
        {
            //allOrders.Add(new OrderModel(order));
            //repo.Add(order);
            repoDB.Create(order);
        }

        public List<OrderModel> GetAll()
        {

            return repo.GetAll();
        }
    }
}