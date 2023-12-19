using OrderProcessing.WebApi.Models;

namespace OrderProcessing.WebApi.Responses
{
    public record FileResponse(
        IReadOnlyCollection<InvoiceDocument> Documents,
        int LineCount,
        int CharCount,
        int Sum,
        int XCount,
        string ProductsWithMaxNetValue);
}
