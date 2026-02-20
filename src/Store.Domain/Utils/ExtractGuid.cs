using Store.Domain.Commands;

namespace Store.Domain.Utils
{
    public static class ExtractGuid
    {
        public static IEnumerable<Guid> Extract(IList<CreateOrderItemCommand> items)
        {
            var guids = new List<Guid>();
            foreach (var item in items)
                guids.Add(Guid.Parse(item.Product.ToString()));
            return guids;
        }
    }
}


