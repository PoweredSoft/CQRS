using PoweredSoft.CQRS.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Queries
{
    public class Person
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class PersonQuery
    {
        public string Search { get; set; }
    }

    public class PersonQueryHandler : IQueryHandler<PersonQuery, IQueryable<Person>>
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

        public Task<IQueryable<Person>> HandleAsync(PersonQuery query, CancellationToken cancellationToken = default)
        {
            var ret = _persons.AsQueryable();

            if (query != null && !string.IsNullOrEmpty(query.Search))
                ret = ret.Where(t => t.FirstName.Contains(query.Search) || t.LastName.Contains(query.Search));

            return Task.FromResult(ret);
        }
    }
}
