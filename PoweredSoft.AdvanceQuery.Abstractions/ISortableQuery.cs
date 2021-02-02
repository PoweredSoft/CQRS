using PoweredSoft.DynamicQuery.Core;
using System.Collections.Generic;

namespace PoweredSoft.AdvanceQuery.Abstractions
{
    public interface ISortableQuery
    {
        List<ISort> GetSorts();
    }
}
