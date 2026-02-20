using Flunt.Validations;

namespace Store.Domain.Entities
{
    public class Product : Entity
    {
        public Product(string title, decimal price, bool active)
        {
            Title = title;
            Price = price;
            Active = active;

            AddNotifications(new Contract()
                .Requires()
                .HasMaxLengthIfNotNullOrEmpty
                (title, 80, "Product.Title.Length", "O titulo não deve ser nullo ou ser maior que 80 caractere !")
                .IsGreaterOrEqualsThan(price, 0.0, "Product.Price", "O preço do produto não pode ser menor que zero"));
        }

        public string Title { get; private set; } = string.Empty;
        public decimal Price { get; private set; }
        public bool Active { get; private set; }
    }
}
