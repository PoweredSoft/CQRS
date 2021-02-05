using System;
using System.Collections.Generic;
using System.Text;

namespace PoweredSoft.CQRS.AspNetCore.OData.Abstractions
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class QueryOdataControllerIgnoreAttribute : Attribute
    {
    }
}
