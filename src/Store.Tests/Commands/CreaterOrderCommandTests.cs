using Store.Domain.Commands;
using Store.Domain.Entities;

namespace Store.Tests.Commands
{
    public class CreaterOrderCommandTests
    {
        [Fact]
        public void Dado_um_comando_valido_o_pedido_deve_ser_gerado()
        {
            // Arrange
            var postOrder = PostOrder();
            postOrder.Customer = "Benjamin";
            postOrder.ZipCode = "12345678";

            // Act
            postOrder.Validate();

            // Assert
            Assert.True(postOrder.Valid);
        }

    
        [Fact]
        public void Dado_um_comando_invalido_o_pedido_nao_deve_ser_gerado()
        {
            // Arrange
            var postOrder = PostOrder();

            // Act
            postOrder.Validate();

            // Assert
            Assert.True(postOrder.Invalid);
        }

        private static CreateOrderCommand PostOrder()
        {
            var postOrder = new CreateOrderCommand()
            {
                Customer = "",
                ZipCode = "",
                PromoCode = "PROMO10",
            };
            postOrder.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 2));

            return postOrder;
        }
    }
}
