using System;

namespace ShopManagementSystem.Customer
{
    internal class CustomerModel
    {
        private string name;
        private string phoneNumber;
        private int age;
        private string address;

        public CustomerModel(string name, string phoneNumber, int age, string address)
        {
            this.name = name;
            this.phoneNumber = phoneNumber;
            this.age = age;
            this.address = address;
        }

        public CustomerModel(CustomerModel otherCustomer)
        {
            name = otherCustomer.name;
            phoneNumber = otherCustomer.phoneNumber;
            age = otherCustomer.age;
            address = otherCustomer.address;
        }

        public override string ToString()
        {
            return name + '~' + phoneNumber + '~' + age + '~' + address;
        }

        public string GetInfo()
        {
            return "Phone Number: " + phoneNumber + ", Age: " + age + ", Address: " + address;
        }

        public string GetName()
        {
            return name;
        }
        public string GetPhoneNumber()
        {
            return phoneNumber;
        }
        public int GetAge()
        {
            return age;
        }
        public string GetAddress()
        {
            return address;
        }
        public void SetName(string name)
        {
            this.name = name;
        }
        public void SetPhoneNumber(string phoneNumber)
        {
            this.phoneNumber = phoneNumber;
        }
        public void SetAge(int age)
        {
            this.age = age;
        }
        public void SetAddress(string address)
        {
            this.address = address;
        }
    }
}