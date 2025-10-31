using System;
using ShopManagementSystem.Product;

namespace ShopManagementSystem.Order
{
    internal class OrderItem
    {
        public int id { get; set; }
        public ProductModel product;
        public int quantity { get; private set; }
        public double totalPrice { get; private set; }

        public OrderItem(ProductModel product, int quantity)
        {
            this.product = new ProductModel(product);
            this.quantity = quantity;
            totalPrice = this.product.CalculateSalePrice() * this.quantity;
        }
        public OrderItem(int id, ProductModel product, int quantity)
        {
            this.id = id;
            this.product = new ProductModel(product);
            this.quantity = quantity;
            totalPrice = this.product.CalculateSalePrice() * this.quantity;
        }

        public OrderItem(OrderItem item)
        {
            id = item.id;
            product = new ProductModel(item.product);
            quantity = item.quantity;
            totalPrice = item.totalPrice;
        }

        public override string ToString()
        {
            return product.GetName() + '`' + product.GetPurchasePrice() + '`' + product.GetDiscount() + '`' + quantity;
        }

        public ProductModel GetProduct()
        {
            return product;
        }
    }
}