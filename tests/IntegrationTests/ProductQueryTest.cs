using Application.Cqrs.Products.Query;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace IntegrationTests
{
    using static Testing;
    public class ProductQueryTest : TestBase
    {
        [Test]
        public async Task ShouldGetProductsList()
        {
            var getProductsQuery = new ProductsQuery();

            var productsList = await SendAsync(getProductsQuery);

            productsList.Should().NotBeNull();
        }
    }
}
