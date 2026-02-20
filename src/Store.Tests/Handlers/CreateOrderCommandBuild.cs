using Bogus;
using Store.Domain.Commands;

namespace Store.Tests.Handlers
{
    public static class CreateOrderCommandBuild
    {
        public static CreateOrderCommand Build()
        {
            var faker = new Faker<CreateOrderCommand>()
                .RuleFor(x => x.Customer, _ => "12345678911")
                .RuleFor(x => x.ZipCode, _ => "12345678")
                .RuleFor(x => x.PromoCode, _ => "12345678")
                .RuleFor(x => x.Items, _ => new List<CreateOrderItemCommand>
                {
                    new CreateOrderItemCommand(Guid.NewGuid(), 1),
                    new CreateOrderItemCommand(Guid.NewGuid(), 1)
                });

            return faker.Generate();
        }
    }
}
