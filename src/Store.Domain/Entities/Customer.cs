using Flunt.Validations;

namespace Store.Domain.Entities
{
    public class Customer : Entity
    {
        public Customer(string name, string email)
        {
            Name = name;
            Email = email;

            AddNotifications(new Contract()
                .Requires()
                .HasMaxLengthIfNotNullOrEmpty
                (name, 80, "Customer.Name.Length.NotNull", "O campo não deve ser nullo ou maior que 80 caractere !"));
        }

        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
    }
}
