namespace OrderProcessing.WebApi.Exceptions
{
    public class InvalidTypeException : Exception
    {
        public InvalidTypeException(int line) : base($"Invalid type in line {line}") { }
    }
}
