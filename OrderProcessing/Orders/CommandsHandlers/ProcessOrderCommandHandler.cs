using OrderProcessing.Contracts.Commands;
using OrderProcessing.Infrastructure.Command;

namespace OrderProcessing.Orders.CommandsHandlers
{
    internal class ProcessOrderCommandHandler : ICommandHandler<ProcessOrderCommand>
    {
        public Task HandleCommand(ProcessOrderCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
