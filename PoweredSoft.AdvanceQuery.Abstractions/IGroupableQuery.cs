using PoweredSoft.DynamicQuery.Core;
using System.Collections.Generic;

namespace PoweredSoft.AdvanceQuery.Abstractions
{
    public interface IGroupableQuery
    {
        List<IGroup> GetGroups();
    }
}
