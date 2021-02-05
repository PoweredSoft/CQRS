using PoweredSoft.CQRS.DynamicQuery.Abstractions;
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
            if (!string.IsNullOrEmpty(safe))
                return Task.FromResult(query.Where(t => t.DisplayName.Contains(safe)));

            return Task.FromResult(query);
        }
    }
}
