using Store.Domain.Entities;
using Store.Domain.Queries;
using Store.Tests.Repositories;

namespace Store.Tests.Queries
{
    public class ProductQueriesTestes
    {
        private IList<Product> _products = [];
        public ProductQueriesTestes()
        {
            _products.Add(new Product("Produto 1", 10, true));
            _products.Add(new Product("Produto 2", 20, true));
            _products.Add(new Product("Produto 3", 30, true));
            _products.Add(new Product("Produto 4", 40, false));
            _products.Add(new Product("Produto 5", 50, false));
        }

        [Fact]
        public void Dado_a_consulta_de_produtos_ativos_deve_retornar_apenas_3()
        {
            var result = _products.AsQueryable().Where(ProductQueries.GetActiveProducts());
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void Dado_a_consulta_de_produtos_inativos_deve_retornar_apenas_2()
        {
            var result = _products.AsQueryable().Where(ProductQueries.GetInactiveProducts());
            Assert.Equal(2, result.Count());
        }


    }
}
