using OrderProcessing.WebApi.Common;
using OrderProcessing.WebApi.Exceptions;
using OrderProcessing.WebApi.Extensions;

namespace OrderProcessing.WebApi.Validators
{
    public interface IArticleInputValidator
    {
        void ValidateInputLine(string inputLine, int lineNumber);
    }

    public class ArticleInputValidator : IArticleInputValidator
    {
        private const int _expectedDocumentSize = 13;

        public void ValidateInputLine(string inputLine, int lineNumber)
        {
            if (inputLine.Split(Consts.FieldSeparator).Length != _expectedDocumentSize)
                throw new InvalidDocumentSize(lineNumber);

            if (!AreTypesValid(inputLine))
                throw new InvalidTypeException(lineNumber);
        }

        private bool AreTypesValid(string inputLine)
        {
            var splittedInput = inputLine.Split(Consts.FieldSeparator);
            return splittedInput[3].CanParseToDecimal()
                && splittedInput[4].CanParseToDecimal()
                && splittedInput[5].CanParseToDecimal()
                && splittedInput[6].CanParseToDecimal()
                && splittedInput[7].CanParseToDecimal()
                && splittedInput[8].CanParseToDecimal()
                && splittedInput[9].CanParseToDecimal()
                && splittedInput[10].CanParseToDecimal();
        }
    }
}
