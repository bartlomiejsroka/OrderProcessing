using OrderProcessing.WebApi.Common;
using OrderProcessing.WebApi.Exceptions;
using OrderProcessing.WebApi.Extensions;

namespace OrderProcessing.WebApi.Validators
{
    public interface IDocumentInputValidator
    {
        void ValidateInputLine(string inputLine, int lineNumber);
    }

    public class DocumentInputValidator : IDocumentInputValidator
    {
        private const int _expectedDocumentSize = 17;

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

            return DateTime.TryParse(splittedInput[4], out var _)
                && DateTime.TryParse(splittedInput[9], out var _)
                && splittedInput[10].CanParseToDecimal()
                && splittedInput[11].CanParseToDecimal()
                && splittedInput[12].CanParseToDecimal()
                && splittedInput[13].CanParseToDecimal()
                && splittedInput[14].CanParseToDecimal()
                && splittedInput[15].CanParseToDecimal();
        }
    }
}
