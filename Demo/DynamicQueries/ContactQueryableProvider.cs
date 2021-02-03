using PoweredSoft.CQRS.DynamicQuery.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.DynamicQueries
{
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
