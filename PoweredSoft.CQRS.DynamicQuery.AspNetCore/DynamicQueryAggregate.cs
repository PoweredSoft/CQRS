using PoweredSoft.DynamicQuery;
using PoweredSoft.DynamicQuery.Core;
using System;

namespace PoweredSoft.CQRS.DynamicQuery.AspNetCore
{
    public class DynamicQueryAggregate
    {
        public string Path { get; set; }
        public string Type { get; set; }

        public IAggregate ToAggregate()
        {
            return new Aggregate
            {
                Type = Enum.Parse<AggregateType>(Type),
                Path = Path
            };
        }

    }
}