using PoweredSoft.CQRS.Abstractions.Attributes;
using System;
using System.Reflection;

namespace PoweredSoft.CQRS.Abstractions.Discovery
{
    public class QueryMeta : IQueryMeta
    {
        public QueryMeta(Type queryType, Type serviceType, Type queryResultType)
        {
            QueryType = queryType;
            ServiceType = serviceType;
            QueryResultType = queryResultType;
        }

        protected virtual QueryNameAttribute NameAttribute => QueryType.GetCustomAttribute<QueryNameAttribute>();

        public virtual string Name
        {
            get
            {
                var name = NameAttribute?.Name ?? QueryType.Name.Replace("Query", string.Empty);
                return name;
            }
        }

        public virtual Type QueryType { get; }
        public virtual Type ServiceType { get; }
        public virtual Type QueryResultType { get; }
    }
}
