using System;
using Microsoft.AspNetCore.Mvc;

namespace PoweredSoft.CQRS.AspNetCore.Mvc
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CommandControllerAuthorizationAttribute : TypeFilterAttribute
    {
        public CommandControllerAuthorizationAttribute() : base(typeof(CommandControllerAsyncAuthorizationFilter))
        {

        }

        public CommandControllerAuthorizationAttribute(Type commandType) : base(typeof(CommandControllerAsyncAuthorizationFilter))
        {
            CommandType = commandType;
        }

        public Type CommandType { get; } = null;
    }
}
