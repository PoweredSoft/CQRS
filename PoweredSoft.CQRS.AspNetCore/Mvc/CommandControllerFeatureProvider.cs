using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using PoweredSoft.CQRS.Abstractions.Discovery;
using PoweredSoft.CQRS.AspNetCore.Abstractions.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace PoweredSoft.CQRS.AspNetCore.Mvc
{
    public class CommandControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        private readonly ServiceProvider serviceProvider;

        public CommandControllerFeatureProvider(ServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            var commandDiscovery = this.serviceProvider.GetRequiredService<ICommandDiscovery>();
            foreach (var f in commandDiscovery.GetCommands())
            {
                var ignoreAttribute = f.CommandType.GetCustomAttribute<CommandControllerIgnoreAttribute>();
                if (ignoreAttribute != null)
                    continue;

                if (f.CommandResultType == null)
                {
                    var controllerType = typeof(CommandController<>).MakeGenericType(f.CommandType);
                    var controllerTypeInfo = controllerType.GetTypeInfo();
                    feature.Controllers.Add(controllerTypeInfo);
                }
                else
                {
                    var controllerType = typeof(CommandController<,>).MakeGenericType(f.CommandType, f.CommandResultType);
                    var controllerTypeInfo = controllerType.GetTypeInfo();
                    feature.Controllers.Add(controllerTypeInfo);
                }
            }
        }
    }
}
