using OrderProcessing.WebApi.Models;

namespace OrderProcessing.Tests.Shared.Builders
{
    public class ArticleBuilder : IObjectBuilder<Article>
    {
        public string Code { get; private set; }
        public  string Name { get; private set; }
        public decimal Quantity { get; private set; }
        public decimal Net { get; private set; }
        public decimal NetValue { get; private set; }
        public decimal Vat { get; private set; }
        public decimal QuantityFrom { get; private set; }
        public decimal AverageFrom { get; private set; }
        public decimal QuantityTo {  get; private set; }
        public decimal AverageTo { get; private set; }
        public string Group { get; private set; }

        public ArticleBuilder()
        {
            Code = string.Empty;
            Name = string.Empty;
            Group = string.Empty;
        }

        public Article Build()
        {
            return new Article(Code,
                Name,
                Quantity,
                Net,
                NetValue,
                Vat,
                QuantityFrom,
                AverageFrom,
                QuantityTo,
                AverageTo,
                Group);
        }

        public ArticleBuilder WithCode(string code) { Code = code; return this; }
        public ArticleBuilder WithName(string name) {  Name = name; return this; }
        public ArticleBuilder WithQuantity(decimal quantity) {  Quantity = quantity; return this; }
        public ArticleBuilder WithNet(decimal net) {  Net = net; return this; }
        public ArticleBuilder WithNetValue(decimal netValue) {  NetValue = netValue; return this; }
        public ArticleBuilder WithVat(decimal vat) {  Vat = vat; return this; }
        public ArticleBuilder WithQuantityFrom(decimal quantityFrom) {  QuantityFrom = quantityFrom; return this; }
        public ArticleBuilder WithAverageFrom(decimal averageFrom) { AverageFrom = averageFrom; return this;}
        public ArticleBuilder WithQuantityTo(decimal quantityTo) {  QuantityTo = quantityTo; return this; }
        public ArticleBuilder WithAverageTo(decimal averageTo) {  AverageTo = averageTo; return this;}
        public ArticleBuilder WithGroup(string group) { Group = group; return this;  }
    }
}
