using System;

namespace PoweredSoft.CQRS.AspNetCore.Abstractions.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CommandControllerIgnoreAttribute : Attribute
    {
    }
}
