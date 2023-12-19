namespace OrderProcessing.Tests.Shared
{
    public interface IObjectBuilder<T>
    {
        T Build();
    }
}
