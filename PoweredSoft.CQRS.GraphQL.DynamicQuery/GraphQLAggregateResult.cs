using PoweredSoft.DynamicQuery.Core;

namespace PoweredSoft.CQRS.GraphQL.DynamicQuery
{
    public class GraphQLAggregateResult
    {
        public string Path { get; set; }
        public AggregateType Type { get; set; }
        public GraphQLVariantResult Value { get; set; }
    }
}
