using PoweredSoft.CQRS.Abstractions;
using PoweredSoft.CQRS.DynamicQuery.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Queries
{
    public class ListPersonQuery
    {
        public string Search { get; set; }
    }

    public class ListPersonQueryHandler : IQueryHandler<ListPersonQuery, List<Person>>
    {
        private readonly IQueryableProvider<Person> provider;

        public ListPersonQueryHandler(IQueryableProvider<Person> provider)
        {
            this.provider = provider;
        }

        public async Task<List<Person>> HandleAsync(ListPersonQuery query, CancellationToken cancellationToken = default)
        {
            var _ = await provider.GetQueryableAsync(query, cancellationToken);

            if (query.Search != null)
                _ = _
                    .Where(t => t.FirstName.Contains(query.Search, StringComparison.InvariantCultureIgnoreCase) ||
                        t.LastName.Contains(query.Search, StringComparison.InvariantCultureIgnoreCase));


            var ret = _.ToList();
            return ret;
        }
    }
}
