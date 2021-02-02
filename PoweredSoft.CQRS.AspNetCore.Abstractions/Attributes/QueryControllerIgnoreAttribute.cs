using System;
using System.Collections.Generic;
using System.Text;

namespace PoweredSoft.CQRS.AspNetCore.Abstractions.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class QueryControllerIgnoreAttribute : Attribute
    {
    }
}
