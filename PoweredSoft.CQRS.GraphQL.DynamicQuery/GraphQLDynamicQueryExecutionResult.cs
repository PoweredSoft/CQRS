using PoweredSoft.DynamicQuery.Core;
using System.Collections.Generic;
using System.Linq;

namespace PoweredSoft.CQRS.GraphQL.DynamicQuery
{
    public class GraphQLDynamicQueryExecutionResult<TResult> : GraphQLDynamicQueryResult<TResult>
    {
        public List<GraphQLDynamicQueryGroupResult<TResult>> Groups { get; set; }
        public long TotalRecords { get; set; }
        public long? NumberOfPages { get; set; }

        public void FromResult(IQueryExecutionResult<TResult> queryResult)
        {
            TotalRecords = queryResult.TotalRecords;
            NumberOfPages = queryResult.NumberOfPages;


            if (queryResult.Aggregates != null)
                Aggregates = queryResult.Aggregates.Select(ConvertAggregateResult).ToList();

            if (queryResult.Data != null)
                Data = queryResult.Data;

            if (queryResult is IQueryExecutionGroupResult<TResult> groupedResult)
                Groups = groupedResult.Groups.Select(ConvertGroupResult).ToList();
        }

        protected virtual GraphQLDynamicQueryGroupResult<TResult> ConvertGroupResult(IGroupQueryResult<TResult> arg)
        {
            var group = new GraphQLDynamicQueryGroupResult<TResult>();

            group.GroupPath = arg.GroupPath;
            group.GroupValue = new GraphQLVariantResult(arg.GroupValue);

            if (arg.Data != null)
                group.Data = arg.Data;

            if (arg.Aggregates != null)
                group.Aggregates = arg.Aggregates.Select(ConvertAggregateResult).ToList();

            if (arg.HasSubGroups)
                group.SubGroups = arg.SubGroups.Select(ConvertGroupResult).ToList();

            return group;
        }

        protected virtual GraphQLAggregateResult ConvertAggregateResult(IAggregateResult arg)
        {
            return new GraphQLAggregateResult
            {
                Path = arg.Path,
                Type = arg.Type,
                Value = new GraphQLVariantResult(arg.Value)
            };
        }

    }
}
