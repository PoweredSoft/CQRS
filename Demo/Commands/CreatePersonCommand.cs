using FluentValidation;
using PoweredSoft.CQRS.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Commands
{
    public class CreatePersonCommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
    {
        public CreatePersonCommandValidator()
        {
            RuleFor(t => t.FirstName).NotEmpty();
            RuleFor(t => t.LastName).NotEmpty();
        }
    }

    public class CreatePersonCommandHandler : ICommandHandler<CreatePersonCommand>
    {
        public Task HandleAsync(CreatePersonCommand command, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
