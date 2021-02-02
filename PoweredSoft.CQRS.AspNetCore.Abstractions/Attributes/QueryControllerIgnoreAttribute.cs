using System;
using System.Collections.Generic;
using System.Text;

namespace PoweredSoft.CQRS.AspNetCore.Abstractions.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class QueryControllerIgnoreAttribute : Attribute
    {
    }
}
