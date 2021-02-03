using Demo.Queries;
using PoweredSoft.CQRS.DynamicQuery.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.DynamicQueries
{
    public class PersonQueryableProvider : IQueryableProvider<Person>
    {
        private readonly IEnumerable<Person> _persons = new List<Person>()
        {
            new Person
            {
                Id = 1,
                FirstName = "David",
                LastName = "Lebee"
            },
            new Person
            {
                Id = 2,
                FirstName = "John",
                LastName = "Doe"
            }
        };

        public Task<IQueryable<Person>> GetQueryableAsync(object query, CancellationToken cancelllationToken = default)
        {
            return Task.FromResult(_persons.AsQueryable());
        }
    }
}
