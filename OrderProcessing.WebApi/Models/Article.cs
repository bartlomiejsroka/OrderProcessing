namespace OrderProcessing.WebApi.Models
{
    public record Article(
        string Code,
        string Name,
        decimal Quantity,
        decimal Net,
        decimal NetValue,
        decimal Vat,
        decimal QuantityFrom,
        decimal AverageFrom,
        decimal QuantityTo,
        decimal AverageTo,
        string Group);
}
