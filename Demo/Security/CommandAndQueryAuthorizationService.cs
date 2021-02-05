using Microsoft.AspNetCore.Http;
using PoweredSoft.CQRS.Abstractions.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Security
{
    public class CommandAndQueryAuthorizationService : IQueryAuthorizationService, ICommandAuthorizationService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public CommandAndQueryAuthorizationService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public Task<AuthorizationResult> IsAllowedAsync(Type queryOrCommandType, CancellationToken cancellationToken = default)
        {
            var authResult = httpContextAccessor.HttpContext.Request.Query["auth-result"].FirstOrDefault();
            if (authResult == "Unauthorized")
                return Task.FromResult(AuthorizationResult.Unauthorized);
            else if (authResult == "Forbidden")
                return Task.FromResult(AuthorizationResult.Forbidden);

            return Task.FromResult(AuthorizationResult.Allowed);
        }
    }
}
