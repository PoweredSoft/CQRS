using PoweredSoft.DynamicQuery;
using PoweredSoft.DynamicQuery.Core;
using System;

namespace PoweredSoft.CQRS.GraphQL.DynamicQuery
{
    public class GraphQLSort
    {
        public string Path { get; set; }
        public bool? Ascending { get; set; }

        internal ISort ToSort()
        {
            return new Sort(Path, Ascending);
        }
    }
}