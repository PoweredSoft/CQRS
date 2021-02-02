using System;

namespace PoweredSoft.CQRS.Abstractions.Discovery
{
    public interface ICommandMeta
    {
        string Name { get; }
        Type CommandType { get; }
        Type ServiceType { get; }
        Type CommandResultType { get; }
    }
}
