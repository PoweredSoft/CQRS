using HotChocolate;
using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace PoweredSoft.CQRS.GraphQL.HotChocolate
{
    public static class RequestExecutorBuilderExtensions
    {
        public static IRequestExecutorBuilder AddPoweredSoftQueries(this IRequestExecutorBuilder builder)
        {
            builder.AddTypeExtension<QueryObjectType>();
            return builder;
        }

        public static IRequestExecutorBuilder AddPoweredSoftMutations(this IRequestExecutorBuilder builder)
        {
            builder.AddTypeExtension<MutationObjectType>();
            return builder;
        }
    }
}
