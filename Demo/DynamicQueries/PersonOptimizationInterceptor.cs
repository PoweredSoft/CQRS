using Demo.Queries;
using PoweredSoft.DynamicQuery;
using PoweredSoft.DynamicQuery.Core;
using System.Collections.Generic;

namespace Demo.DynamicQueries
{
    public class PersonOptimizationInterceptor : IFilterInterceptor, ISortInterceptor
    {
        public IFilter InterceptFilter(IFilter filter)
        {
            if (filter is ISimpleFilter simpleFilter)
            {
                if (simpleFilter.Path.Equals(nameof(PersonModel.FullName), System.StringComparison.InvariantCultureIgnoreCase))
                    return new CompositeFilter
                    {
                        Type = filter.Type,
                        And = filter.And,
                        Filters = new List<IFilter> {
                            new SimpleFilter
                            {
                                Not = simpleFilter.Not,
                                And = false,
                                Type = simpleFilter.Type,
                                Value = simpleFilter.Value,
                                Path = nameof(Person.FirstName)
                            },
                            new SimpleFilter
                            {
                                Not = simpleFilter.Not,
                                And = false,
                                Type = simpleFilter.Type,
                                Value = simpleFilter.Value,
                                Path = nameof(Person.LastName)
                            }
                        }
                    };
            }

            return filter;
        }

        public IEnumerable<ISort> InterceptSort(IEnumerable<ISort> sort)
        {
            foreach(var s in sort)
            {
                if (s.Path.Equals(nameof(PersonModel.FullName), System.StringComparison.InvariantCultureIgnoreCase))
                    yield return new Sort(nameof(Person.LastName), s.Ascending);
                else
                    yield return s;
            }
        }
    }
}
