using System;
using Microsoft.AspNetCore.Mvc;

namespace PoweredSoft.CQRS.AspNetCore.Mvc
{
    [AttributeUsage(AttributeTargets.Method)]
    public class QueryControllerAuthorizationAttribute : TypeFilterAttribute
    {
        public QueryControllerAuthorizationAttribute() : base(typeof(QueryControllerAsyncAuthorizationFilter))
        {
            
        }

        public QueryControllerAuthorizationAttribute(Type queryType) : base(typeof(QueryControllerAsyncAuthorizationFilter))
        {
            QueryType = queryType;
        }

        public Type QueryType { get; } = null;
    }
}
