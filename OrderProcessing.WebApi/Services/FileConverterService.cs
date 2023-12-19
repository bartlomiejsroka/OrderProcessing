using OrderProcessing.WebApi.Enums;
using OrderProcessing.WebApi.Exceptions;
using OrderProcessing.WebApi.Models;
using OrderProcessing.WebApi.Responses;
using OrderProcessing.WebApi.Validators;

namespace OrderProcessing.WebApi.Services
{
    public interface IFileConverterService
    {
        FileResponse GetDocumentsFromFile(IFormFile file, int articleNumberGreaterThan);
    }

    public class FileConverterService : IFileConverterService
    {
        private readonly IRowTypeValidator _rowTypeValidator;
        private readonly IDocumentInputValidator _documentInputValidator;
        private readonly IInvoiceDocumentBuilderService _invoiceDocumentBuilderService;
        private readonly IArticleInputValidator _articleInputValidator;
        private readonly IArticleBuilderService _articleBuilderService;

        public FileConverterService(IRowTypeValidator rowTypeValidator,
            IDocumentInputValidator documentInputValidator,
            IInvoiceDocumentBuilderService invoiceDocumentBuilderService,
            IArticleInputValidator articleInputValidator,
            IArticleBuilderService articleBuilderService)
        {
            _rowTypeValidator = rowTypeValidator;
            _documentInputValidator = documentInputValidator;
            _invoiceDocumentBuilderService = invoiceDocumentBuilderService;
            _articleInputValidator = articleInputValidator;
            _articleBuilderService = articleBuilderService;
        }

        public FileResponse GetDocumentsFromFile(IFormFile file, int articleNumberGreaterThan)
        {
            var stream = file.OpenReadStream();

            if (stream == null || stream.Length == 0)
            {
                throw new EmptyFileException();
            }

            var totalCharacterCount = 0;
            var totalLineCount = 0;
            var documents = new List<InvoiceDocument>();

            using (var reader = new StreamReader(stream))
            {
                string? line;
                InvoiceDocument? document = null;
                var articles = new List<Article>();

                while ((line = reader.ReadLine()) != null)
                {
                    var type = line[0].ToRowType();
                    _rowTypeValidator.ValidateFirstHeaderRow(document, type);

                    if (type == RowType.Document)
                    {
                        var documentWithArticles = _invoiceDocumentBuilderService.AppendArticles(document, articles);
                        AddDocument(documentWithArticles, documents!);
                        articles = new List<Article>();
                        _documentInputValidator.ValidateInputLine(line, totalLineCount);
                        document = _invoiceDocumentBuilderService.BuildDocument(line);
                    }

                    if (type == RowType.Article)
                    {
                        _articleInputValidator.ValidateInputLine(line, totalLineCount);
                        var article = _articleBuilderService.BuildArticle(line);
                        articles.Add(article);
                    }

                    totalCharacterCount += line.Length;
                    totalLineCount++;
                }
            }

            GetDocumentsInfo(articleNumberGreaterThan, documents, out var articlesSum, out var documentsWithHigherArticleNumberThanGiven, out var itemsWithMaxNetValue);

            return new FileResponse(documents!, totalLineCount, totalCharacterCount, articlesSum, documentsWithHigherArticleNumberThanGiven, string.Join(",", itemsWithMaxNetValue));
        }

        private static void GetDocumentsInfo(int articleNumberGreaterThan,
            List<InvoiceDocument>? documents,
            out int articlesSum,
            out int documentsWithHigherArticleNumberThanGiven,
            out List<string> itemsWithMaxNetValue)
        {
            articlesSum = documents!.Sum(x => x.Articles.Count);
            documentsWithHigherArticleNumberThanGiven = documents!.Where(x => x.Articles.Count > articleNumberGreaterThan).Count();
            itemsWithMaxNetValue = documents!
                            .SelectMany(x => x.Articles)
                            .Where(x => x.NetValue ==
                                documents!.SelectMany(x => x.Articles)
                                .Max(x => x.NetValue))
                            .Select(x => x.Name).ToList();
        }

        private static void AddDocument(InvoiceDocument? documentWithArticles, List<InvoiceDocument> documents)
        {
            if(documentWithArticles != null)
                documents.Add(documentWithArticles);
        }
    }
}
