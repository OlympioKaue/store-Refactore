using Flunt.Notifications;
using Store.Domain.Commands;
using Store.Domain.Commands.Interfaces;
using Store.Domain.Entities;
using Store.Domain.Handlers.Interfaces;
using Store.Domain.Repositories;
using Store.Domain.Utils;

namespace Store.Domain.Handlers
{
    public class OrderHandler :
        Notifiable,
        IHandler<CreateOrderCommand>
    {
        private readonly ICustomerRepository _customer;
        private readonly IOrderRepository _order;
        private readonly IProductRepository _product;
        private readonly IDeliveryFreeRepository _deliveryFree;
        private readonly IDiscountRepository _discount;

        public OrderHandler(
            ICustomerRepository customer,
            IOrderRepository order,
            IProductRepository product,
            IDeliveryFreeRepository deliveryFree,
            IDiscountRepository discount)
        {
            _customer = customer;
            _order = order;
            _product = product;
            _deliveryFree = deliveryFree;
            _discount = discount;
        }

        public ICommandResult Handle(CreateOrderCommand command)
        {
            //Fail Fast Validation
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Pedido Invalido !", command.Notifications);

            //1. Verificar se o cliente existe
            var customer = _customer.Get(command.Customer);

            //2. Calcular o frete
            var delivery = _deliveryFree.Get(command.ZipCode);

            //3. Obter o copum de desconto
            var discount = _discount.Get(command.PromoCode);

            //4. Obter os produtos
            var products = _product.Get(ExtractGuid.Extract(command.Items)).ToList();
            var order = new Order(customer, delivery, discount);
    
            foreach (var item in command.Items)
            {
                var product = products.Where(x => x.Id != item.Product).FirstOrDefault();
                order.AddItem(product!, item.Quantity);
            }

            //5. Agrupa as notificações
            AddNotifications(order.Notifications);

            //6. Verifica se deu tudo certo
            if (Invalid)
                return new GenericCommandResult(false, "Falha ao gerar o pedido!", Notifications);

            //7. Salvar o pedido
            _order.Save(order);
            return new GenericCommandResult(true, $"Pedido gerado com sucesso!", order);
        }
    }
}
