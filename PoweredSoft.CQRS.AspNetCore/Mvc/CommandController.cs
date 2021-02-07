using Microsoft.AspNetCore.Mvc;
using PoweredSoft.CQRS.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PoweredSoft.CQRS.AspNetCore.Mvc
{
    [Produces("application/json")]
    [ApiController, Route("api/command/[controller]")]
    public class CommandController<TCommand> : Controller
        where TCommand : class
    {
        [HttpPost, CommandControllerAuthorization]
        public async Task<IActionResult> Handle([FromServices] ICommandHandler<TCommand> handler, 
            [FromBody] TCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await handler.HandleAsync(command, this.Request.HttpContext.RequestAborted);
            return Ok();
        }
    }

    [Produces("application/json")]
    [ApiController, Route("api/command/[controller]")]
    public class CommandController<TCommand, TTCommandResult> : Controller
        where TCommand : class
    {
        [HttpPost, CommandControllerAuthorization]
        public async Task<ActionResult<TTCommandResult>> Handle([FromServices] ICommandHandler<TCommand, TTCommandResult> handler,
            [FromBody] TCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await handler.HandleAsync(command, this.Request.HttpContext.RequestAborted));
        }
    }
}
