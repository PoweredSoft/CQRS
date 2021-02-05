using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using PoweredSoft.CQRS.Abstractions;
using PoweredSoft.CQRS.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace PoweredSoft.CQRS.AspNetCore.OData
{
    [Route("api/odata/[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class QueryODataController<TQuery, TQueryResult> : ODataController
        where TQuery : class
    {
        [EnableQuery, HttpGet, QueryControllerAuthorization]
        public async Task<TQueryResult> Get([FromServices]IQueryHandler<TQuery, TQueryResult> queryHandler)
        {
            var result = await queryHandler.HandleAsync(null, HttpContext.RequestAborted);
            return result;
        }
    }

}
