using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.Edm;
using PoweredSoft.CQRS.Abstractions.Discovery;
using PoweredSoft.CQRS.AspNetCore.OData.Abstractions;
using System.Linq;
using System.Reflection;

namespace PoweredSoft.CQRS.AspNetCore.Mvc
{
    public static class EndpointExstensions
    {
        public static IEdmModel GetPoweredSoftODataEdmModel(this IEndpointRouteBuilder endpoint)
        {
            var queryDiscovery = endpoint.ServiceProvider.GetRequiredService<IQueryDiscovery>();
          
            var odataBuilder = new ODataConventionModelBuilder();
            odataBuilder.EnableLowerCamelCase();

            foreach(var q in queryDiscovery.GetQueries())
            {
                var ignoreAttribute = q.QueryType.GetCustomAttribute<QueryOdataControllerIgnoreAttribute>();
                if (ignoreAttribute != null)
                    continue;

                if (q.Category != "BasicQuery")
                    continue;

                var isQueryable = q.QueryResultType.Namespace == "System.Linq" && q.QueryResultType.Name.Contains("IQueryable");
                if (!isQueryable)
                    continue;


                var entityType = q.QueryResultType.GetGenericArguments().First();
                odataBuilder.GetType().GetMethod("EntitySet").MakeGenericMethod(entityType).Invoke(odataBuilder, new object[] {
                    q.LowerCamelCaseName
                });
            }

            return odataBuilder.GetEdmModel();
        }
    }
}
