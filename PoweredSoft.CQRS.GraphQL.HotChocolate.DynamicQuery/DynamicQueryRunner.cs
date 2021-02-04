using PoweredSoft.CQRS.DynamicQuery;
using PoweredSoft.CQRS.DynamicQuery.Abstractions;
using PoweredSoft.CQRS.GraphQL.DynamicQuery;
using System.Threading;
using System.Threading.Tasks;

namespace PoweredSoft.CQRS.GraphQL.HotChocolate.DynamicQuery
{
    public class DynamicQueryRunner<TSource, TDestination>
        where TSource : class
        where TDestination : class
    {
        private readonly DynamicQueryHandler<TSource, TDestination> handler;

        public DynamicQueryRunner(DynamicQueryHandler<TSource, TDestination> handler)
        {
            this.handler = handler;
        }

        public async Task<GraphQLDynamicQueryExecutionResult<TDestination>> RunAsync(IDynamicQuery<TSource, TDestination> query, CancellationToken cancellationToken = default)
        {
            var result = await handler.HandleAsync(query);
            var outcome = new GraphQLDynamicQueryExecutionResult<TDestination>();
            outcome.FromResult(result);
            return outcome;
        }
    }
}