using Demo.Queries;
using PoweredSoft.DynamicQuery.Core;
using System.Linq;

namespace Demo.DynamicQueries
{
    public class PersonConvertInterceptor : IQueryConvertInterceptor<Person, PersonModel>
    {
        public PersonModel InterceptResultTo(Person entity)
        {
            return new PersonModel
            {
                Id = entity.Id,
                FullName = entity.FirstName + " " + entity.LastName
            };
        }
    }
}
