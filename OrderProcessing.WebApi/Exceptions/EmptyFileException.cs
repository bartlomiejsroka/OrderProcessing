namespace OrderProcessing.WebApi.Exceptions
{
    public class EmptyFileException : Exception
    {
        public EmptyFileException() : base("File cannot be empty") {}
    }
}
