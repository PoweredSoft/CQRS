using PoweredSoft.DynamicQuery;
using PoweredSoft.DynamicQuery.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PoweredSoft.CQRS.GraphQL.DynamicQuery
{
    public class GraphQLAdvanceQueryFilter
    {
        public bool? And { get; set; }
        public FilterType Type { get; set; }
        public string Path { get; set; }
        public GraphQLVariantInput Value { get; set; }
        public bool? Not { get; set; }

        public List<GraphQLAdvanceQueryFilter> Filters { get; set; }

        internal IFilter ToFilter()
        {
            if (Type == FilterType.Composite)
            {
                var ret = new CompositeFilter
                {
                    And = And,
                    Type = FilterType.Composite
                };

                if (Filters == null)
                    ret.Filters = new List<IFilter>();
                else
                    ret.Filters = Filters.Select(t => t.ToFilter()).ToList();

                return ret;
            }
            else
            {
                return new SimpleFilter
                {
                    And = And,
                    Type = Type,
                    Not = Not,
                    Path = Path,
                    Value = Value.GetRawObjectValue()
                };
            }
        }
    }
}
