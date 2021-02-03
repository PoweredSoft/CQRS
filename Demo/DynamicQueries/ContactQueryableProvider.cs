using PoweredSoft.CQRS.DynamicQuery.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.DynamicQueries
{
    public class SearchContactParamsService : IAlterQueryableService<Contact, Contact, SearchContactParams>
    {
        public Task<IQueryable<Contact>> AlterQueryableAsync(IQueryable<Contact> query, IDynamicQueryParams<SearchContactParams> dynamicQuery, CancellationToken cancellationToken = default)
        {
            var safe = dynamicQuery.GetParams()?.SearchDisplayName;
            return Task.FromResult(query.Where(t => t.DisplayName.Contains(safe)));
        }
    }

    public class ContactQueryableProvider : IQueryableProvider<Contact>
    {
        public Task<IQueryable<Contact>> GetQueryableAsync(object query, CancellationToken cancelllationToken = default)
        {
            var source = new List<Contact>
            {
                new Contact { Id = 1, DisplayName = "David L"},
                new Contact { Id = 2, DisplayName = "John Doe"} 
            };

            var ret = source.AsQueryable();
            return Task.FromResult(ret);
        }
    }
}
