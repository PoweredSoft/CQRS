using HotChocolate.Types;
using PoweredSoft.CQRS.Abstractions;
using PoweredSoft.CQRS.Abstractions.Discovery;
using PoweredSoft.CQRS.DynamicQuery.Discover;
using System;

namespace PoweredSoft.CQRS.GraphQL.HotChocolate.DynamicQuery
{

    internal class DynamicQueryObjectType : ObjectTypeExtension
    {
        private readonly IQueryDiscovery queryDiscovery;

        public DynamicQueryObjectType(IQueryDiscovery queryDiscovery) : base()
        {
            this.queryDiscovery = queryDiscovery;
        }

        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            base.Configure(descriptor);
            descriptor.Name("Query");

            foreach(var q in queryDiscovery.GetQueries())
            {
                if (q.Category == "DynamicQuery" && q is DynamicQueryMeta dq)
                {
                    var f = descriptor.Field(q.LowerCamelCaseName);

                    // service to execute with.
                    var queryHandlerServiceType = typeof(IQueryHandler<,>).MakeGenericType(dq.QueryType, dq.QueryResultType);

                    // destermine argument type.
                    Type argumentType;
                    Type runnerType;
                    if (dq.ParamsType != null)
                    {
                        argumentType = typeof(GraphQL.DynamicQuery.GraphQLDynamicQuery<,,>).MakeGenericType(
                            dq.SourceType, dq.DestinationType, dq.ParamsType);

                        runnerType = typeof(DynamicQueryRunnerWithParams<,,>)
                            .MakeGenericType(dq.SourceType, dq.DestinationType, dq.ParamsType);
                    }
                    else
                    {
                        argumentType = typeof(GraphQL.DynamicQuery.GraphQLDynamicQuery<,>).MakeGenericType(
                            dq.SourceType, dq.DestinationType);

                        runnerType = typeof(DynamicQueryRunner<,>)
                            .MakeGenericType(dq.SourceType, dq.DestinationType);
                    }

                    f.Argument("params", a => a
                        .Type(argumentType)
                        .DefaultValue(Activator.CreateInstance(argumentType))
                        );

                    // make generic type of outgoing type.
                    var resultType = typeof(GraphQL.DynamicQuery.GraphQLDynamicQueryExecutionResult<>)
                        .MakeGenericType(dq.DestinationType);

                    f.Type(resultType);

                    // resolver
                    f.Resolve(async r =>
                    {
                        dynamic argument = r.ArgumentValue<object>("params");
                       
                        // handler service.
                        var service = r.Service(queryHandlerServiceType);

                        // runner. 
                        dynamic runner = Activator.CreateInstance(runnerType, new object[] { service });

                        // get outcome.
                        object outcome = await runner.RunAsync(argument, r.RequestAborted);

                        return outcome;
                    });
                }
            }
        }
    }
}