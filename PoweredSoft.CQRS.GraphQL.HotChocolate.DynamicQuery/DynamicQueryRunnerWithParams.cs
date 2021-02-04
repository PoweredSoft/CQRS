using PoweredSoft.CQRS.DynamicQuery;
using PoweredSoft.CQRS.DynamicQuery.Abstractions;
using PoweredSoft.CQRS.GraphQL.DynamicQuery;
using System.Threading;
using System.Threading.Tasks;

namespace PoweredSoft.CQRS.GraphQL.HotChocolate.DynamicQuery
{
    public class DynamicQueryRunnerWithParams<TSource, TDestination, TParams>
        where TSource : class
        where TDestination : class
        where TParams : class
    {
        private readonly DynamicQueryHandler<TSource, TDestination, TParams> handler;

        public DynamicQueryRunnerWithParams(DynamicQueryHandler<TSource, TDestination, TParams> handler)
        {
            this.handler = handler;
        }

        public async Task<GraphQLDynamicQueryExecutionResult<TDestination>> RunAsync(IDynamicQuery<TSource, TDestination, TParams> query, CancellationToken cancellationToken = default)
        {
            var result = await handler.HandleAsync(query);
            var outcome = new GraphQLDynamicQueryExecutionResult<TDestination>();
            outcome.FromResult(result);
            return outcome;
        }
    }
}