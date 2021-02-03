using PoweredSoft.CQRS.Abstractions;
using PoweredSoft.CQRS.DynamicQuery.Abstractions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Queries
{
    public class PersonQueryHandler : IQueryHandler<PersonQuery, IQueryable<Person>>
    {
        private readonly IQueryableProvider<Person> queryableProvider;

        public PersonQueryHandler(IQueryableProvider<Person> queryableProvider)
        {
            this.queryableProvider = queryableProvider;
        }

        public async Task<IQueryable<Person>> HandleAsync(PersonQuery query, CancellationToken cancellationToken = default)
        {
            var ret = await queryableProvider.GetQueryableAsync(query);

            if (query != null && !string.IsNullOrEmpty(query.Search))
                ret = ret.Where(t => t.FirstName.Contains(query.Search) || t.LastName.Contains(query.Search));

            return ret;
        }
    }
}
