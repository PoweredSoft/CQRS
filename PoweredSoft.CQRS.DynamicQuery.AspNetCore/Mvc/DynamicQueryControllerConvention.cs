using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;
using PoweredSoft.CQRS.Abstractions.Discovery;
using System;
using System.Collections.Generic;
using System.Text;

namespace PoweredSoft.CQRS.DynamicQuery.AspNetCore.Mvc
{
    public class DynamicQueryControllerConvention : IControllerModelConvention
    {
        private readonly IServiceProvider serviceProvider;

        public DynamicQueryControllerConvention(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Apply(ControllerModel controller)
        {
            if (controller.ControllerType.IsGenericType && controller.ControllerType.Name.Contains("DynamicQueryController") && controller.ControllerType.Assembly == typeof(DynamicQueryControllerConvention).Assembly)
            {
                var genericType = controller.ControllerType.GenericTypeArguments[0];
                var queryDiscovery = this.serviceProvider.GetRequiredService<IQueryDiscovery>();
                var query = queryDiscovery.FindQuery(genericType);
                controller.ControllerName = query.LowerCamelCaseName;
            }
        }
    }
}
