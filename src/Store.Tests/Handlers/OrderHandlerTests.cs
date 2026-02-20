using Shouldly;
using Store.Domain.Commands;
using Store.Domain.Entities;
using Store.Domain.Handlers;
using Store.Domain.Repositories;
using Store.Domain.Utils;
using Store.Tests.Repositories;

namespace Store.Tests.Handlers
{
    public class OrderHandlerTests
    {
        private readonly ICustomerRepository _customer;
        private readonly IOrderRepository _order;
        private readonly IProductRepository _product;
        private readonly IDeliveryFreeRepository _deliveryFree;
        private readonly IDiscountRepository _discount;

        public OrderHandlerTests()
        {
            _customer = new FakerCustomerRepository();
            _order = new FakerOrderRepository();
            _product = new FakerProductRepository();
            _deliveryFree = new FakerDeliveryFreeRepository();
            _discount = new FakerDiscountRepository();
        }

        [Fact]
        public void Dado_um_comando_um_pedido_deve_ser_gerado()
        {
            var command = CreateOrderCommandBuild.Build();
            var handler = GetOrderHandler(command);

            var result = (GenericCommandResult)handler.Handle(command);

            result.ShouldSatisfyAllConditions(
               () => result.Sucess.ShouldBeTrue(),
               () => result.Message.ShouldContain($"Pedido gerado com sucesso!"));
        }

        [Fact]
        public void Dado_um_cep_invalido_o_pedido_deve_ser_gerado_normalmente()
        {
            var command = CreateOrderCommandBuild.Build();
            command.ZipCode = "00000000";

            var handler = GetOrderHandler(command);
            var result =(GenericCommandResult) handler.Handle(command);

            result.ShouldSatisfyAllConditions(
                () => result.Sucess.ShouldBeTrue(),
                () => result.Message.ShouldContain($"Pedido gerado com sucesso!"));
            
        }

        [Fact]
        public void Dado_um_promocode_invalido_o_pedido_deve_ser_gerado_normalmente()
        {
            var command = CreateOrderCommandBuild.Build();
            command.PromoCode = "87654321";

            var handler = GetOrderHandler(command);
            var result = (GenericCommandResult)handler.Handle(command);

            result.ShouldSatisfyAllConditions(
                () => result.Sucess.ShouldBeTrue(),
                () => result.Message.ShouldContain($"Pedido gerado com sucesso!"));

        }


        [Fact]
        public void Dado_um_comando_invalido_um_pedido_nao_deve_ser_gerado()
        {
            var command = CreateOrderCommandBuild.Build();
            command.Customer = "";
            var handler = GetOrderHandler(command);

            var result = (GenericCommandResult)handler.Handle(command);

            result.ShouldSatisfyAllConditions(
               () => result.Sucess.ShouldBeFalse(),
               () => result.Message.ShouldContain("Pedido Invalido !"));
        }

        //Handler
        public OrderHandler GetOrderHandler(CreateOrderCommand command)
        {
            return new OrderHandler(_customer, _order, _product, _deliveryFree, _discount);
        }
    }
}
