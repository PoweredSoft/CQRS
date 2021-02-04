using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace PoweredSoft.CQRS.GraphQL.HotChocolate.DynamicQuery
{
    public static class RequestExecutorBuilderExtensions
    {
        public static IRequestExecutorBuilder AddPoweredSoftDynamicQueries(this IRequestExecutorBuilder builder)
        {
            builder.AddTypeExtension<DynamicQueryObjectType>();
            return builder;
        }
    }
}
