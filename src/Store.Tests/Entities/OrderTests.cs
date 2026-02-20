using Store.Domain.Entities;

namespace Store.Tests.Entities
{
    public class OrderTests
    {
        private readonly Customer _customer = new Customer(name: "Kauê Olympio", "kaueolympio@yahoo.com");
        private readonly Product _product = new Product("Curso C# Completo", 600.00m, true);
        private readonly Discount _discount = new Discount(0.0m, DateTime.Now.AddDays(5));


        [Fact]
        public void Dado_um_desconto_de_10_o_valor_do_pedido_deve_ser_60()
        {
            var discount = new Discount(10, DateTime.Now.AddDays(5));
            var order = new Order(_customer, 10, discount);
            var product = new Product("Linha", 10m, true);
            order.AddItem(product, 4);

            Assert.Equal(60m, order.Total());
        }

        [Fact]
        public void Dado_uma_taxa_de_entrega_de_10_o_valor_do_pedido_deve_ser_60()
        {
            var order = new Order(_customer, 10, _discount);
            var product = new Product("Linha", 10m, true);
            order.AddItem(product, 5);

            Assert.Equal(60m, order.Total());
        }

        [Fact]
        public void Dado_Um_Novo_Pedido_valido_ele_deve_gerar_um_numero_de_8_caracetere()
        {
            var order = new Order(_customer, 0.0m, null!);

            Assert.Equal(8, order.Number.Length!);
        }


        [Fact]
        public void Dado_um_pedido_sem_cliente_o_mesmo_deve_ser_invalido()
        {
            var order = new Order(null!, 10m, _discount);
            Assert.False(order.Valid);
        }

        [Fact]
        public void Dado_um_novo_pedido_seu_status_deve_ser_aguardando_pagamento()
        {
            var order = new Order(_customer, 0.0m, null!);

            Assert.True(order.OrderStatus == Domain.Enums.EOrderStatus.WaitingPayment);
        }

        [Fact]
        public void Dado_um_novo_pedido_seu_status_deve_ser_aguardando_entrega()
        {
            var order = new Order(_customer, 0.0m, null!);
            order.AddItem(_product, 5);
            order.Pay(3000.00m);

            Assert.True(order.OrderStatus == Domain.Enums.EOrderStatus.WaitingDelivery);
        }

        [Fact]
        public void Dado_um_pedido_cancelado_seu_status_deve_ser_cancelado()
        {
            var order = new Order(_customer, 0.0m, null!);
            order.Cancel();

            Assert.True(order.OrderStatus == Domain.Enums.EOrderStatus.Canceled);
        }

        [Fact]
        public void Dado_um_novo_item_sem_produto_o_mesmo_nao_deve_ser_adicionado()
        {
            var order = new Order(_customer, 0.0m, null!);
            order.AddItem(null!, 0);

            Assert.False(order.Valid);
        }

        [Fact]
        public void Dado_um_novo_valor_amount_menor_que_zero_no_pagamento_nao_deve_ser_adicionado()
        {
            var order = new Order(_customer, 0.0m, null!);
            order.AddItem(_product, 1);
            order.Pay(0);

            Assert.False(order.Valid);
        }

        [Fact]
        public void Dado_um_desconto_o_mesmo_deve_estar_expirado()
        {
            var discount = new Discount(10m, DateTime.Now.AddDays(-1));
            Assert.False(discount.IsValid());
        }

        [Fact]
        public void Dado_um_pedido_valido_seu_total_deve_ser_50()
        {
            var order = new Order(_customer, 0.0m, _discount);
            order.AddItem(_product, 5);

            Assert.Equal(3000m, order.Total());
        }

        [Fact]
        public void Dado_um_desconto_expirado_o_valor_do_pedido_deve_ser_60()
        {
            var expireDiscount = new Discount(0.0m, DateTime.Now.AddDays(-1));
            var order = new Order(_customer, 10m, _discount);
            var product = new Product("Capinha de Celular", 10m, true);

            order.AddItem(product, 5);
            Assert.Equal(60m, order.Total());

        }

        [Fact]
        public void Dado_um_desconto_invalido_o_valor_do_pedido_deve_ser_60()
        {
            var order = new Order(_customer, 0, null!);
            order.AddItem(_product, 5);
            Assert.Equal(3000m, order.Total());
        }
    }
}
