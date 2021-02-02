using PoweredSoft.CQRS.Abstractions.Attributes;
using System;
using System.Reflection;

namespace PoweredSoft.CQRS.Abstractions.Discovery
{
    public class CommandMeta : ICommandMeta
    {
        public CommandMeta(Type commandType, Type serviceType, Type commandResultType)
        {
            CommandType = commandType;
            ServiceType = serviceType;
            CommandResultType = commandResultType;
        }

        public CommandMeta(Type commandType, Type serviceType)
        {
            CommandType = commandType;
            ServiceType = serviceType;
        }

        protected virtual CommandNameAttribute NameAttribute => CommandType.GetCustomAttribute<CommandNameAttribute>();

        public virtual string Name
        {
            get
            {
                var name = NameAttribute?.Name ?? CommandType.Name.Replace("Command", string.Empty);
                return name;
            }
        }

        public virtual Type CommandType { get; }
        public virtual Type ServiceType { get; }
        public virtual Type CommandResultType { get; }
    }
}
