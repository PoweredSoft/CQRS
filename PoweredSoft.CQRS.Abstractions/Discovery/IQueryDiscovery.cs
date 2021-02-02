using System;
using System.Collections.Generic;
using System.Text;

namespace PoweredSoft.CQRS.Abstractions.Discovery
{
    public interface IQueryDiscovery
    {
        IQueryMeta FindQuery(string name);
        IQueryMeta FindQuery(Type queryType);
        IEnumerable<IQueryMeta> GetQueries();
        bool QueryExists(string name);
        bool QueryExists(Type queryType);
    }

    public interface ICommandDiscovery
    {
        bool CommandExists(string name);
        bool CommandExists(Type commandType);
        ICommandMeta FindCommand(string name);
        ICommandMeta FindCommand(Type commandType);
        IEnumerable<ICommandMeta> GetCommands();
    }
}
