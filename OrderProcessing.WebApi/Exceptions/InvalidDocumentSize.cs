namespace OrderProcessing.WebApi.Exceptions
{
    public class InvalidDocumentSize : Exception
    {
        public InvalidDocumentSize(int line) : base($"Invalid document size in line {line}") { }
    }
}
