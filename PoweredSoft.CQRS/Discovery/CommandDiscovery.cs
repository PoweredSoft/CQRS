using PoweredSoft.CQRS.Abstractions.Discovery;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PoweredSoft.CQRS.Discovery
{
    public class CommandDiscovery : ICommandDiscovery
    {
        private readonly IEnumerable<ICommandMeta> commandMetas;

        public CommandDiscovery(IEnumerable<ICommandMeta> commandMetas)
        {
            this.commandMetas = commandMetas;
        }

        public virtual IEnumerable<ICommandMeta> GetCommands() => commandMetas;
        public virtual ICommandMeta FindCommand(string name) => commandMetas.FirstOrDefault(t => t.Name == name);
        public virtual ICommandMeta FindCommand(Type commandType) => commandMetas.FirstOrDefault(t => t.CommandType == commandType);
        public virtual bool CommandExists(string name) => commandMetas.Any(t => t.Name == name);
        public virtual bool CommandExists(Type commandType) => commandMetas.Any(t => t.CommandType == commandType);
    }
}
