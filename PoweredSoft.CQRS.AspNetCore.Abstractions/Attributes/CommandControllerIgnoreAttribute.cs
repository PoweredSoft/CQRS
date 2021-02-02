using System;

namespace PoweredSoft.CQRS.AspNetCore.Abstractions.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class CommandControllerIgnoreAttribute : Attribute
    {
    }
}
