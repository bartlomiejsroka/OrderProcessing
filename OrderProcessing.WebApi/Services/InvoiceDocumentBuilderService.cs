using OrderProcessing.WebApi.Common;
using OrderProcessing.WebApi.Extensions;
using OrderProcessing.WebApi.Models;

namespace OrderProcessing.WebApi.Services
{
    public interface IInvoiceDocumentBuilderService
    {
        InvoiceDocument BuildDocument(string line);
        InvoiceDocument? AppendArticles(InvoiceDocument? invoiceDocument, IReadOnlyCollection<Article> articles);
    }

    public class InvoiceDocumentBuilderService : IInvoiceDocumentBuilderService
    {
        public InvoiceDocument? AppendArticles(InvoiceDocument? invoiceDocument, IReadOnlyCollection<Article> articles)
            => invoiceDocument != null ? new InvoiceDocument(
                invoiceDocument.Ba,
                invoiceDocument.Type,
                invoiceDocument.DocumentNumber,
                invoiceDocument.OperationDateTime,
                invoiceDocument.DayNumber,
                invoiceDocument.ContractorName,
                invoiceDocument.ContractorNumber,
                invoiceDocument.ExternalDocumentNumber,
                invoiceDocument.ExternalDocumentDateTime,
                invoiceDocument.Net,
                invoiceDocument.Vat,
                invoiceDocument.Gross,
                invoiceDocument.F1,
                invoiceDocument.F2,
                invoiceDocument.F3,
                articles) : null;

        public InvoiceDocument BuildDocument(string line)
        {
            var inputData = line.Split(Consts.FieldSeparator);

            return new InvoiceDocument(
                Ba: inputData[1],
                Type: inputData[2],
                DocumentNumber: inputData[3],
                OperationDateTime: DateTime.Parse(inputData[4]),
                DayNumber: inputData[5],
                ContractorNumber: inputData[6],
                ContractorName: inputData[7],
                ExternalDocumentNumber: inputData[8],
                ExternalDocumentDateTime: DateTime.Parse(inputData[9]),
                Net:inputData[10].ParseToDecimal(),
                Vat: inputData[11].ParseToDecimal(),
                Gross: inputData[12].ParseToDecimal(),
                F1: inputData[13].ParseToDecimal(),
                F2: inputData[14].ParseToDecimal(),
                F3: inputData[15].ParseToDecimal(),
                Articles: Array.Empty<Article>()
                );
        }
    }
}
