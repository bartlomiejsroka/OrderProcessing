namespace OrderProcessing.WebApi.Models
{
    public record InvoiceDocument(
        string Ba,
        string Type,
        string DocumentNumber,
        DateTime OperationDateTime,
        string DayNumber,
        string ContractorNumber,
        string ContractorName,
        string ExternalDocumentNumber,
        DateTime ExternalDocumentDateTime,
        decimal Net,
        decimal Vat,
        decimal Gross,
        decimal F1,
        decimal F2,
        decimal F3,
        IReadOnlyCollection<Article> Articles);
}
