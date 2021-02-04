using FluentValidation.Results;
using PoweredSoft.CQRS.GraphQL.Abstractions;
using System.Collections.Generic;

namespace PoweredSoft.CQRS.GraphQL.FluentValidation
{
    public class GraphQLFluentValidationResult : IGraphQLValidationResult
    {
        public bool IsValid => Errors.Count == 0;
        public List<IGraphQLFieldError> Errors { get; } = new List<IGraphQLFieldError>();

        public static GraphQLFluentValidationResult From(ValidationResult result)
        {
            var model = new GraphQLFluentValidationResult();
            foreach (var error in result.Errors)
            {
                var fieldError = new GraphQLFieldError
                {
                    Field = error.PropertyName
                };
                fieldError.Errors.Add(error.ErrorMessage);
                model.Errors.Add(fieldError);
            }

            return model;
        }
    }
}
