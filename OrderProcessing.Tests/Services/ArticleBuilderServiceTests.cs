using FluentAssertions;
using OrderProcessing.Tests.Shared.Builders;
using OrderProcessing.Tests.Shared.Extension;
using OrderProcessing.WebApi.Services;

namespace OrderProcessing.Tests.Services
{
    public class ArticleBuilderServiceTests
    {
        private readonly IArticleBuilderService articleBuilderService;

        public ArticleBuilderServiceTests()
        {
            articleBuilderService = new ArticleBuilderService();
        }

        [Fact]
        public void GivenValidaData_WhenBuildArticle_ThenArticleReturned()
        {
            //given 
            var article = new ArticleBuilder()
                .WithCode("code")
                .WithName("name")
                .WithNet(2.2m)
                .WithNetValue(5.5m)
                .WithVat(11.11m)
                .WithQuantity(1)
                .WithAverageFrom(5.5m)
                .WithAverageTo(2.2m)
                .WithQuantityFrom(1.1m)
                .WithQuantityTo(2.2m)
                .WithGroup("group")
                .Build();

            var input = $"H,{article.Code},{article.Name},{article.Quantity.ToStringWithDot()},{article.Net.ToStringWithDot()}," +
                $"{article.NetValue.ToStringWithDot()},{article.Vat.ToStringWithDot()},{article.QuantityFrom.ToStringWithDot()}," +
                $"{article.AverageFrom.ToStringWithDot()},{article.QuantityTo.ToStringWithDot()},{article.AverageTo.ToStringWithDot()},{article.Group}";

            //when
            var result = articleBuilderService.BuildArticle(input);

            //then
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(article);
        }
    }
}
