using Moq;
using Store.Domain.Entities;
using Store.Domain.Repositories;

namespace Store.Tests.Repositories
{
    public class FakerCustomerRepository : ICustomerRepository
    {
        public Customer Get(string document)
        {
            if (document is "12345678911")
                return new Customer("Bruce", "Wayne");

            return null!;
        }
    }
}
