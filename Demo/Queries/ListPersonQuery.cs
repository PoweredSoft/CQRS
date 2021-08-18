using PoweredSoft.CQRS.Abstractions;
using PoweredSoft.CQRS.DynamicQuery.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Queries
{
    public class OnePersonQuery
    {
        public long PersonId { get; set; }
    }

    public class OnePersonQueryHandler : IQueryHandler<OnePersonQuery, Person>
    {
        private readonly IQueryableProvider<Person> provider;

        public OnePersonQueryHandler(IQueryableProvider<Person> provider)
        {
            this.provider = provider;
        }

        public async Task<Person> HandleAsync(OnePersonQuery query, CancellationToken cancellationToken = default)
        {
            var _ = await provider.GetQueryableAsync(query, cancellationToken);
            var ret = _.First(t => t.Id == query.PersonId);
            return ret;
        }
    }
}
