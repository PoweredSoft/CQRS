using PoweredSoft.DynamicQuery.Core;
using System.Collections.Generic;

namespace PoweredSoft.AdvanceQuery.Abstractions
{
    public interface IAggregatableQuery
    {
        List<IAggregate> GetAggregates();
    }
}
