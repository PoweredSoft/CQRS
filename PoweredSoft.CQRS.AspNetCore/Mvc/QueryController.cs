using Microsoft.AspNetCore.Mvc;
using PoweredSoft.CQRS.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PoweredSoft.CQRS.AspNetCore.Mvc
{
    [ApiController, Route("api/query/[controller]")]
    public class QueryController<TQuery, TQueryResult> : Controller
        where TQuery : class
    {
        [HttpPost, QueryControllerAuthorization]
        public async Task<ActionResult<TQueryResult>> Handle([FromServices] IQueryHandler<TQuery, TQueryResult> handler, 
            [FromBody] TQuery query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            return Ok(await handler.HandleAsync(query, this.Request.HttpContext.RequestAborted));
        }
    }
}
