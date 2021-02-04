using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;
using PoweredSoft.CQRS.Abstractions.Discovery;
using System;

namespace PoweredSoft.CQRS.AspNetCore.Mvc
{
    public class CommandControllerConvention : IControllerModelConvention
    {
        private readonly IServiceProvider serviceProvider;

        public CommandControllerConvention(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Apply(ControllerModel controller)
        {
            if (controller.ControllerType.IsGenericType && controller.ControllerType.Name.Contains("CommandController") && controller.ControllerType.Assembly == typeof(CommandControllerConvention).Assembly)
            {
                var genericType = controller.ControllerType.GenericTypeArguments[0];
                var commandDiscovery = this.serviceProvider.GetRequiredService<ICommandDiscovery>();
                var command = commandDiscovery.FindCommand(genericType);
                controller.ControllerName = command.LowerCamelCaseName;
            }
        }
    }
}
