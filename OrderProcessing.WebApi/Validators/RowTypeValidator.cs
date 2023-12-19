using OrderProcessing.WebApi.Enums;
using OrderProcessing.WebApi.Exceptions;
using OrderProcessing.WebApi.Models;

namespace OrderProcessing.WebApi.Validators
{
    public interface IRowTypeValidator
    {
        void ValidateFirstHeaderRow(InvoiceDocument? document, RowType currentRow);
    }
    public class RowTypeValidator : IRowTypeValidator
    {
        public void ValidateFirstHeaderRow(InvoiceDocument? document, RowType currentRow)
        {
            if (document == null && currentRow == RowType.Article)
                throw new InvalidRowException();
        }
    }
}
