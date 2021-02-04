using System.Collections.Generic;
using System.Linq;

namespace PoweredSoft.CQRS.GraphQL.DynamicQuery
{
    public class GraphQLDynamicQueryGroupResult<TResult> : GraphQLDynamicQueryResult<TResult>
    {
        public string GroupPath { get; set; }
        public GraphQLVariantResult GroupValue { get; set; }
        public bool HasSubGroups => SubGroups?.Any() == true;
        public List<GraphQLDynamicQueryGroupResult<TResult>> SubGroups { get; set; }
    }
}
