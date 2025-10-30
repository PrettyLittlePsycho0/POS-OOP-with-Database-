using System;

namespace ShopManagementSystem.Product
{
    internal class ProductModel
    {
        private string name;
        private double purchasePrice;
        private double discount;

        public ProductModel(string name, double purchasePrice)
        {
            this.name = name;
            this.purchasePrice = purchasePrice;
            discount = 0;
        }
        public ProductModel(string name, double purchasePrice, double discount)
        {
            this.name = name;
            this.purchasePrice = purchasePrice;
            this.discount = discount;
        }

        public ProductModel(ProductModel otherProduct)
        {
            name = otherProduct.name;
            purchasePrice = otherProduct.purchasePrice;
            discount = otherProduct.discount;
        }

        public double CalculateSalePrice()
        {
            return purchasePrice - (purchasePrice * discount / 100);
        }
        public override string ToString()
        {
            return name + '~' + purchasePrice + '~' + discount;
        }

        public string GetInfo()
        {
            return "Purchase Price: $" + purchasePrice + ", Discount: " + discount + "%, Sale Price: $" + CalculateSalePrice();
        }

        public string GetName()
        {
            return name;
        }
        public double GetPurchasePrice()
        {
            return purchasePrice;
        }
        public double GetDiscount()
        {
            return discount;
        }

        public void SetName(string name)
        {
            this.name = name;
        }
        public void SetPurchasePrice(double amount)
        {
            purchasePrice = amount;
        }
        public void SetDiscount(double percentageAmount)
        {
            discount = percentageAmount;
        }
    }
}