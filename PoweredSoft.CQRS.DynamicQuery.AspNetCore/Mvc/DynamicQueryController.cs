using Microsoft.AspNetCore.Mvc;
using PoweredSoft.CQRS.AspNetCore.Mvc;
using PoweredSoft.CQRS.DynamicQuery.Abstractions;
using PoweredSoft.DynamicQuery.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PoweredSoft.CQRS.DynamicQuery.AspNetCore.Mvc
{
    [ApiController, Route("api/query/[controller]")]
    public class DynamicQueryController<TUnderlyingQuery, TSource, TDestination> : Controller
        where TSource : class
        where TDestination : class
    {
        [HttpPost, QueryControllerAuthorization]
        public async Task<IQueryExecutionResult<TDestination>> HandleAsync(
                [FromBody] DynamicQuery<TSource, TDestination> query, 
                [FromServices]PoweredSoft.CQRS.Abstractions.IQueryHandler<IDynamicQuery<TSource, TDestination>, IQueryExecutionResult<TDestination>> queryHandler
            )
        {
            var result = await queryHandler.HandleAsync(query, HttpContext.RequestAborted);
            return result;
        }
    }

    [ApiController, Route("api/query/[controller]")]
    public class DynamicQueryController<TUnderlyingQuery, TSource, TDestination, TParams> : Controller
        where TSource : class
        where TDestination : class
        where TParams : class
    {
        [HttpPost, QueryControllerAuthorization]
        public async Task<IQueryExecutionResult<TDestination>> HandleAsync(
                [FromBody] DynamicQuery<TSource, TDestination, TParams> query,
                [FromServices] PoweredSoft.CQRS.Abstractions.IQueryHandler<IDynamicQuery<TSource, TDestination, TParams>, IQueryExecutionResult<TDestination>> queryHandler
            )
        {
            var result = await queryHandler.HandleAsync(query, HttpContext.RequestAborted);
            return result;
        }
    }
}
