using System;

namespace PoweredSoft.AdvanceQuery.Abstractions
{

    public interface IPageableQuery
    {
        int? GetPage();
        int? GetPageSize();
    }
}
