using PoweredSoft.CQRS.Abstractions.Discovery;
using PoweredSoft.CQRS.Discovery;
using System;
using System.Collections.Generic;
using System.Text;

namespace PoweredSoft.CQRS.DynamicQuery.Discover
{
    public class DynamicQueryMeta : QueryMeta
    {
        public DynamicQueryMeta(Type queryType, Type serviceType, Type queryResultType) : base(queryType, serviceType, queryResultType)
        {

        }

        public Type SourceType => QueryType.GetGenericArguments()[0];
        public Type DestinationType => QueryType.GetGenericArguments()[1];
        public override string Category => "DynamicQuery";
        public override string Name
        {
            get
            {
                if (OverridableName != null)
                    return OverridableName;

                var pluralizer = new Pluralize.NET.Pluralizer();
                return pluralizer.Pluralize(DestinationType.Name);
            }
        }

        public Type ParamsType { get; internal set; }
        public string OverridableName { get; internal set; }
    }
}
