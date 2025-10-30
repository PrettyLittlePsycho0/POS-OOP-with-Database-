using System;
using ShopManagementSystem.Common;

namespace ShopManagementSystem.Customer
{
    internal class CustomerRepository
    {
        private readonly string file = "Customers.txt"; 

        public void Add(CustomerModel customer)
        {
            string filePath = FileUtils.GetPath(file);
            if (!File.Exists(filePath)) File.Create(filePath).Close();
            bool isEmpty = FileUtils.IsFileEmpty(filePath);

            using (StreamWriter file = new StreamWriter(filePath, true))
            {
                if (!isEmpty) file.WriteLine();
                file.Write(customer.ToString());
            }
        }

        public void SaveAll(List<CustomerModel> customers)
        {
            string filePath = FileUtils.GetPath(file);
            if (!File.Exists(filePath)) File.Create(filePath).Close();

            using (StreamWriter file = new StreamWriter(filePath))
            {
                for (int i = 0; i < customers.Count; i++)
                {
                    if (i != 0) file.WriteLine();
                    file.Write(customers[i].ToString());
                }
            }
        }

        public List<CustomerModel> GetAll()
        {
            string filePath = FileUtils.GetPath(file);
            if (!File.Exists(filePath)) File.Create(filePath).Close();
            List<CustomerModel> customers = new List<CustomerModel>();

            foreach (string record in File.ReadLines(filePath))
            {
                if (!string.IsNullOrWhiteSpace(record))
                {
                    CustomerModel temp = ParseCustomerRecord(record);
                    if (temp != null)
                    {
                        customers.Add(temp);
                    }
                }
            }

            return customers;
        }
        

        private CustomerModel ParseCustomerRecord(string record)
        {
            string name = FileUtils.ParseRecord(record, 1, '~');
            if (name == "") return null;
            string phoneNumber = FileUtils.ParseRecord(record, 2, '~');
            if (!int.TryParse(FileUtils.ParseRecord(record, 3, '~'), out int age)) return null;
            string address = FileUtils.ParseRecord(record, 4, '~');
            return new CustomerModel(name, phoneNumber, age, address);
        }
    }
}