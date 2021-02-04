using PoweredSoft.DynamicQuery;
using PoweredSoft.DynamicQuery.Core;
using System;

namespace PoweredSoft.CQRS.GraphQL
{
    public class GraphQLAdvanceQueryGroup 
    {
        public string Path { get; set; }
        public bool? Ascending { get; set; }

        internal IGroup ToGroup()
        {
            return new Group
            {
                Path = Path,
                Ascending = Ascending
            };
        }
    }
}
