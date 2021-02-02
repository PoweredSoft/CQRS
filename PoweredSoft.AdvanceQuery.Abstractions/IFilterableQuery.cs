using PoweredSoft.DynamicQuery.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PoweredSoft.AdvanceQuery.Abstractions
{
    public interface IFilterableQuery
    {
        List<IFilter> GetFilters();
    }
}
