using System;

namespace PoweredSoft.CQRS.Abstractions.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class QueryNameAttribute : Attribute
    {
        public QueryNameAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
