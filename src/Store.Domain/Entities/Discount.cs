using Flunt.Validations;

namespace Store.Domain.Entities
{
    public class Discount : Entity
    {
        public Discount(decimal amount, DateTime expireDate)
        {
            Amount = amount;
            ExpireDate = expireDate;

            AddNotifications(new Contract()
                .Requires()
                .IsGreaterOrEqualsThan(amount, 0.0, "Discount.Amount", "O amount deve ser maior que zero"));
        }

        public decimal Amount { get; private set; }
        public DateTime ExpireDate { get; private set; } 

        public bool IsValid()
        {   
            return DateTime.Compare(DateTime.Now, ExpireDate) < 0;
        }

        public decimal Value()
        {
            if (IsValid())
                return Amount;
            else
                return 0;
        }
    }
}
