using PoweredSoft.CQRS.Abstractions.Discovery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PoweredSoft.CQRS.Discovery
{
    public class QueryDiscovery : IQueryDiscovery
    {
        private readonly IEnumerable<IQueryMeta> queryMetas;

        public QueryDiscovery(IEnumerable<IQueryMeta> queryMetas)
        {
            this.queryMetas = queryMetas;
        }

        public virtual IEnumerable<IQueryMeta> GetQueries() => queryMetas;
        public virtual IQueryMeta FindQuery(string name) => queryMetas.FirstOrDefault(t => t.Name == name);
        public virtual IQueryMeta FindQuery(Type queryType) => queryMetas.FirstOrDefault(t => t.QueryType == queryType);
        public virtual bool QueryExists(string name) => queryMetas.Any(t => t.Name == name);
        public virtual bool QueryExists(Type queryType) => queryMetas.Any(t => t.QueryType == queryType);
    }
}
