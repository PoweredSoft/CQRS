using System;
using System.Collections.Generic;
using System.Text;

namespace PoweredSoft.CQRS.Abstractions.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class CommandNameAttribute : Attribute
    {
        public CommandNameAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
