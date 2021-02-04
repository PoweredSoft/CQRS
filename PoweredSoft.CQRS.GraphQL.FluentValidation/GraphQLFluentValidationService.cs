using FluentValidation;
using PoweredSoft.CQRS.GraphQL.Abstractions;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PoweredSoft.CQRS.GraphQL.FluentValidation
{
    public class GraphQLFluentValidationService : IGraphQLValidationService
    {
        private readonly IServiceProvider serviceProvider;

        public GraphQLFluentValidationService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task<IGraphQLValidationResult> ValidateAsync<T>(T subject, CancellationToken cancellationToken = default)
        {
            var validationService = serviceProvider.GetService(typeof(IValidator<T>)) as IValidator<T>;
            if (validationService == null)
                return new GraphQLValidResult();

            var result = await validationService.ValidateAsync(subject, cancellationToken);
            if (!result.IsValid)
                return GraphQLFluentValidationResult.From(result);

            return new GraphQLValidResult();
        }

        public async Task<IGraphQLValidationResult> ValidateObjectAsync(object subject, CancellationToken cancellationToken = default)
        {
            var validatorType = typeof(IValidator<>).MakeGenericType(subject.GetType());
            var validationService = serviceProvider.GetService(validatorType) as IValidator;
            if (validationService == null)
                return new GraphQLValidResult();

            var result = await validationService.ValidateAsync(new ValidationContext<object>(subject), cancellationToken);
            if (!result.IsValid)
                return GraphQLFluentValidationResult.From(result);

            return new GraphQLValidResult();
        }
    }
}
