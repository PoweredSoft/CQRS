using HotChocolate.Language;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using PoweredSoft.CQRS.Abstractions;
using PoweredSoft.CQRS.Abstractions.Discovery;
using System;
using System.Text;

namespace PoweredSoft.CQRS.GraphQL.HotChocolate
{
    public class QueryObjectType : ObjectTypeExtension
    {
        private readonly IQueryDiscovery queryDiscovery;

        public QueryObjectType(IQueryDiscovery queryDiscovery) : base()
        {
            this.queryDiscovery = queryDiscovery;
        }

        protected override void Configure(IObjectTypeDescriptor desc)
        {
            desc.Name("Query");
            foreach (var q in queryDiscovery.GetQueries())
            {
                if (q.Category != "BasicQuery")
                    return;

                var queryField = desc.Field(q.LowerCamelCaseName);
                var typeToGet = typeof(IQueryHandler<,>).MakeGenericType(q.QueryType, q.QueryResultType);

                queryField.Type(q.QueryResultType);

                // TODO.
                // always required.
                //queryField.Use((sp, d) => new QueryAuthorizationMiddleware(q.QueryType, d));

                if (q.QueryType.GetProperties().Length == 0)
                {
                    queryField.Resolve(async ctx =>
                    {
                        var queryArgument = Activator.CreateInstance(q.QueryType);
                        return await HandleQuery(ctx, typeToGet, queryArgument);
                    });

                    continue;
                }

                queryField.Argument("params", t => t.Type(q.QueryType));

                queryField.Resolve(async ctx =>
                {
                    var queryArgument = ctx.ArgumentValue<object>("params");
                    return await HandleQuery(ctx, typeToGet, queryArgument);
                });

                /*
                if (q.QueryObjectRequired)
                    queryField.Use<QueryParamRequiredMiddleware>();*/

                /* TODO
                if (q.ValidateQueryObject)
                    queryField.Use<QueryValidationMiddleware>();
                */
            }
        }

        private async System.Threading.Tasks.Task<object> HandleQuery(IResolverContext resolverContext, Type typeToGet, object queryArgument)
        {
            dynamic service = resolverContext.Service(typeToGet);
            var result = await service.HandleAsync((dynamic)queryArgument);
            return result;
        }
    }
}