using PoweredSoft.Data.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.AsyncProvider
{
    public class InMemoryQueryableHandler : IAsyncQueryableHandlerService
    {
        public Task<bool> AnyAsync<T>(IQueryable<T> queryable, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(queryable.Any(predicate));
        }

        public Task<bool> AnyAsync<T>(IQueryable<T> queryable, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(queryable.Any());
        }

        public bool CanHandle<T>(IQueryable<T> queryable)
        {
            var result = queryable is EnumerableQuery<T>;
            return result;
        }

        public Task<int> CountAsync<T>(IQueryable<T> queryable, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(queryable.Count());
        }

        public Task<T> FirstOrDefaultAsync<T>(IQueryable<T> queryable, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(queryable.FirstOrDefault());
        }

        public Task<T> FirstOrDefaultAsync<T>(IQueryable<T> queryable, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(queryable.FirstOrDefault(predicate));
        }

        public Task<long> LongCountAsync<T>(IQueryable<T> queryable, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(queryable.LongCount());
        }

        public Task<List<T>> ToListAsync<T>(IQueryable<T> queryable, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(queryable.ToList());
        }
    }
}
