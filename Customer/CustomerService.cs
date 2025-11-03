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

        public bool Create(CustomerModel customer)
        {
            if (repoDB.GetByPhoneNumber(customer.phoneNumber) != null)
            {
                ConsoleUtiles.PauseForKeyPress("Customer with this number already exists.");
                return false;
            }
            return repoDB.Create(customer);
        }

        public bool Update(CustomerModel updated)
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
            return repoDB.Update(new CustomerModel(updated));
        }

        public bool Delete(int id)
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
            return repoDB.Delete(id);
        }

        public List<CustomerModel> GetAll()
        {
            return repoDB.GetAll();
        }

        public CustomerModel GetCustomerById(int id)
        {
            return repoDB.GetCustomerById(id);
        }

        public List<CustomerModel> GetCustomerByName(string name)
        {
            return repoDB.GetByName(name);
        }
        public List<CustomerModel> GetCustomerByFirstChar(string ch)
        {
            return repoDB.GetByFirstChar(ch);
        }
        public CustomerModel GetCustomerByPhoneNumber(string phoneNumber)
        {
            return repoDB.GetByPhoneNumber(phoneNumber);
        }

        public List<CustomerModel> GetCustomerByAddress(string address)
        {
            return repoDB.GetByAddress(address);
        }

        public List<CustomerModel> GetCustomerByAge(int age)
        {
            return repoDB.GetByAge(age);
        }

        public bool Exists(int id)
        {
            /*foreach (CustomerModel customer in allCustomers)
            {
                if (customer.GetName() == name) return true;
            }
            return false;*/
            return repoDB.Exists(id);
        }
    }
}