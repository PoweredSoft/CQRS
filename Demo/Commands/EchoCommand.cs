using FluentValidation;
using PoweredSoft.CQRS.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Commands
{
    public class EchoCommand
    {
        public string Message { get; set; }
    }

    public class EchoCommandValidator : AbstractValidator<EchoCommand>
    {
        public EchoCommandValidator()
        {
            RuleFor(t => t.Message).NotEmpty();
        }
    }

    public class EchoCommandHandler : ICommandHandler<EchoCommand, string>
    {
        public Task<string> HandleAsync(EchoCommand command, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(command.Message);
        }
    }
}
