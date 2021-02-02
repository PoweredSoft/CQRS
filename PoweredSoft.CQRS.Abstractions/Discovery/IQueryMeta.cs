using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace PoweredSoft.CQRS.Abstractions.Discovery
{
    public interface IQueryMeta
    {
        string Name { get; }
        Type QueryType { get; }
        Type ServiceType { get; }
        Type QueryResultType { get; }
    }
}
