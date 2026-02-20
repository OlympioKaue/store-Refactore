using Flunt.Validations;

namespace Store.Domain.Entities
{
    public class OrderItem : Entity
    {
        public OrderItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;

            AddNotifications(new Contract()
                .Requires()
                .IsNotNull(product, "OrderItem.Product", "Produto invalido")
                .IsGreaterOrEqualsThan(quantity, 0, "OrderItem.Quantity", "A quantidade em estoque não pode ser menor que zero"));

            if (product != null)
                Price = product.Price;

            //Price = Product != null ? product.Price : 0;
        }

        public Product Product { get; private set; } = default!;
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }

        public decimal Total()
        {
            return Price * Quantity;
        }
    }
}
