using Moq;
using Store.Domain.Repositories;

namespace Store.Tests.Repositories
{
    public class FakerDeliveryFreeRepository : IDeliveryFreeRepository
    {
        public decimal Get(string zipCode)
        {
            return 10;
        }
    }
}
