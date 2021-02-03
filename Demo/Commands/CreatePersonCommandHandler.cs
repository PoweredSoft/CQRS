using PoweredSoft.CQRS.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Commands
{
    public class CreatePersonCommandHandler : ICommandHandler<CreatePersonCommand>
    {
        public Task HandleAsync(CreatePersonCommand command, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
