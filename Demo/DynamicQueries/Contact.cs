using PoweredSoft.CQRS.DynamicQuery.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.DynamicQueries
{
    public class Contact
    {
        public long Id { get; set; }
        public string DisplayName { get; set; }
    }

    public class ContactQueryableProvider : IQueryableProvider<Contact>
    {
        public Task<IQueryable<Contact>> GetQueryableAsync(object query, CancellationToken cancelllationToken = default)
        {
            var ret = new List<Contact>
            {
                new Contact { Id = 1, DisplayName = "David L"},
                new Contact { Id = 2, DisplayName = "John Doe"} 
            };

            return Task.FromResult(ret.AsQueryable());
        }
    }
}
