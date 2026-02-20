using Flunt.Validations;
using Store.Domain.Enums;

namespace Store.Domain.Entities
{
    public class Order : Entity
    {
        private readonly IList<OrderItem> _items = [];
        public Order(Customer customer, decimal deliveryFree, Discount discount)
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNotNull(customer, "Order.Customer", "Cliente invalido")
                .IsGreaterOrEqualsThan(deliveryFree, 0, "Order.Delivery", "O delivery não pode ser menor quero 0"));

            Customer = customer;
            Date = DateTime.Now;
            Number = Guid.NewGuid().ToString().Substring(0, 8);
            DeliveryFree = deliveryFree;
            OrderStatus = EOrderStatus.WaitingPayment;
            Discount = discount;
        }

        public Customer Customer { get; private set; } = default!;
        public DateTime Date { get; private set; }
        public string Number { get; private set; } = string.Empty;
        public IReadOnlyCollection<OrderItem> Items => _items.ToArray();
        public decimal DeliveryFree { get; private set; }
        public EOrderStatus OrderStatus { get; private set; }
        public Discount Discount { get; private set; }

        public void AddItem(Product product, int quantity)
        {
            var item = new OrderItem(product, quantity);
            AddNotifications(item); //order pega as notificações de orderItem.
         
            if (item.Valid)
                _items.Add(item);
        }

        public decimal Total()
        {
            decimal total = 0;
            foreach (var item in Items)
            {
                total += item.Total();
            }

            total += DeliveryFree;
            total += Discount != null ? Discount.Value() : 0;

            return total;
        }

        public void Pay(decimal amount) // preço que vou pagar.
        {
            AddNotifications(new Contract()
                .Requires()
                .IsGreaterThan(amount, 0, "Order.Amount", "O amount deve ser maior que zero"));

            if (Invalid)
                return;

            if (amount == Total())
                this.OrderStatus = EOrderStatus.WaitingDelivery;

        }

        public void Cancel()
        {
            OrderStatus = EOrderStatus.Canceled;
        }
    }
}
