using System;
using System.Threading;
using System.Threading.Tasks;

namespace PoweredSoft.CQRS.Abstractions
{
    public interface ICommandHandler<TCommand>
        where TCommand : class
    {
        Task HandleAsync(TCommand command, CancellationToken cancellationToken = default);
    }

    public interface ICommandHandler<TCommand, TCommandResult>
        where TCommand : class
    {
        Task<TCommandResult> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
    }
}
