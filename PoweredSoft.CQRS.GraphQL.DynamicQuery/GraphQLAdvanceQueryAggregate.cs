using PoweredSoft.DynamicQuery;
using PoweredSoft.DynamicQuery.Core;
using System;

namespace PoweredSoft.CQRS.GraphQL
{
    public class GraphQLAdvanceQueryAggregate
    {
        public string Path { get; set; }
        public AggregateType Type { get; set; }

        internal IAggregate ToAggregate()
        {
            return new Aggregate
            {
                Path = Path,
                Type = Type
            };
        }
    }
}
