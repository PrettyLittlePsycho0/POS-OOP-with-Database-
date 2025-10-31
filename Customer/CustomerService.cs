using ShopManagementSystem.Common;

namespace ShopManagementSystem.Customer
{
    internal class CustomerService
    {
        private List<CustomerModel> allCustomers;

        private CustomerRepository repo;
        private CustomerRepositoryDB repoDB;
 
        public CustomerService()
        {
            repoDB = new CustomerRepositoryDB();
            repo = new CustomerRepository();
            allCustomers = repo.GetAll();
        }

        public void Add(CustomerModel customer)
        {
            repoDB.Create(customer);
        }

        public void Update(CustomerModel updated)
        {
            /*for (int i = 0; i < allCustomers.Count; i++)
            {
                if (allCustomers[i].GetName() == updated.GetName())
                {
                    allCustomers[i] = new CustomerModel(updated);
                    break;
                }
            }
            repo.SaveAll(allCustomers);*/
        }

        public void Delete(string name)
        {
            /*foreach (CustomerModel customer in allCustomers)
            {
                if (customer.GetName() == name)
                {
                    allCustomers.Remove(customer);
                    repo.SaveAll(allCustomers);
                    break;
                }
            }*/
        }

        public List<CustomerModel> GetAll()
        {
            return allCustomers;
        }

        public CustomerModel GetCustomerByName(string name)
        {
            foreach (CustomerModel customer in allCustomers)
            {
                if (customer.GetName() == name) return customer;
            }
            return null;
        }
        public List<CustomerModel> GetCustomerByFirstChar(string ch)
        {
            List<CustomerModel> customers = new List<CustomerModel>();
            foreach (CustomerModel customer in allCustomers)
            {
                if (customer.GetName().ToLower().StartsWith(ch.ToLower())) customers.Add(customer);
            }
            return customers;
        }
        public CustomerModel GetCustomerByPhoneNumber(string phoneNumber)
        {
            foreach (CustomerModel customer in allCustomers)
            {
                if (customer.GetPhoneNumber() == phoneNumber) return customer;
            }
            return null;
        }

        public List<CustomerModel> GetCustomerByAddress(string address)
        {
            List<CustomerModel> customers = new List<CustomerModel>();
            foreach (CustomerModel customer in allCustomers)
            {
                if (customer.GetAddress().ToLower() == address.ToLower()) customers.Add(customer);
            }
            return customers;
        }

        public List<CustomerModel> GetCustomerByAge(int age)
        {
            List<CustomerModel> customers = new List<CustomerModel>();
            foreach (CustomerModel customer in allCustomers)
            {
                if (customer.GetAge() == age) customers.Add(customer);
            }
            return customers;
        }

        public bool Exists(string name)
        {
            foreach (CustomerModel customer in allCustomers)
            {
                if (customer.GetName() == name) return true;
            }
            return false;
        }
    }
}