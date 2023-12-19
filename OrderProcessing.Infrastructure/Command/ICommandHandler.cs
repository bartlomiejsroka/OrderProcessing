namespace OrderProcessing.Infrastructure.Command
{
    public interface ICommandHandler<T> where T : ICommand
    {
        Task HandleCommand(T command);
    }
}
