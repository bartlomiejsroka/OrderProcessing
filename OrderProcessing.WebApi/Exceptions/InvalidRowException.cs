namespace OrderProcessing.WebApi.Exceptions
{
    public class InvalidRowException : Exception
    {
        public InvalidRowException() : base("Article has to come with document first"){ }
    }
}
