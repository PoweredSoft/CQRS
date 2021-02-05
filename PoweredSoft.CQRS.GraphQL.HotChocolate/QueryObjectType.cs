using HotChocolate.Language;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using PoweredSoft.CQRS.Abstractions;
using PoweredSoft.CQRS.Abstractions.Discovery;
using System;
using System.Collections.Generic;
using System.Linq;
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

                queryField.Use((sp, d) => new QueryAuthorizationMiddleware(q.QueryType, d));

                // if its a IQueryable.
                if (q.QueryResultType.Namespace == "System.Linq" && q.QueryResultType.Name.Contains("IQueryable"))
                {
                    //waiting on answer to be determined.
                    /*var genericArgument = q.QueryResultType.GetGenericArguments().First();
                    var type = new ListType(new NonNullType(new NamedTypeNode));
                    queryField.Type(type);
                    queryField.UsePaging();
                    */

                    queryField.Type(q.QueryResultType);
                }
                else
                {
                    queryField.Type(q.QueryResultType);

                }

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

                queryField.Use<QueryValidationMiddleware>();
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