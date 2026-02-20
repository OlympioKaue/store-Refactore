namespace Store.Domain.Repositories
{
    public interface IDeliveryFreeRepository
    {
        decimal Get(string zipCode);
    }
}
