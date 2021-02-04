using System.Collections.Generic;

namespace PoweredSoft.CQRS.GraphQL.DynamicQuery
{
    public class GraphQLDynamicQueryResult<TResult>
    {
        public List<TResult> Data { get; set; }
        public List<GraphQLAggregateResult> Aggregates { get; set; }
    }
}
