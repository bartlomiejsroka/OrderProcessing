using OrderProcessing.WebApi.Common;
using OrderProcessing.WebApi.Extensions;
using OrderProcessing.WebApi.Models;

namespace OrderProcessing.WebApi.Services
{
    public interface IArticleBuilderService
    {
        Article BuildArticle(string line);
    }
    public class ArticleBuilderService : IArticleBuilderService
    {
        public Article BuildArticle(string line)
        {
            var inputData = line.Split(Consts.FieldSeparator);

            return new Article(Code: inputData[1],
                Name: inputData[2],
                Quantity: inputData[3].ParseToDecimal(),
                Net: inputData[4].ParseToDecimal(),
                NetValue: inputData[5].ParseToDecimal(),
                Vat: inputData[6].ParseToDecimal(),
                QuantityFrom: inputData[7].ParseToDecimal(),
                AverageFrom: inputData[8].ParseToDecimal(),
                QuantityTo: inputData[9].ParseToDecimal(),
                AverageTo: inputData[10].ParseToDecimal(),
                Group: inputData[11]);
        }
    }
}
