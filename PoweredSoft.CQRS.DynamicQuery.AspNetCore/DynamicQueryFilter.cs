using Microsoft.AspNetCore.Mvc;
using PoweredSoft.DynamicQuery;
using PoweredSoft.DynamicQuery.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace PoweredSoft.CQRS.DynamicQuery.AspNetCore
{
    public class DynamicQueryFilter
    {
        public List<DynamicQueryFilter> Filters { get; set; }
        public bool? And { get; set; }
        public string Type { get; set; }
        public bool? Not { get; set; }
        public string Path { get; set; }
        public object Value { get; set; }

        [FromQuery(Name ="value")]
        public string QueryValue
        {
            get
            {
                return null;
            }
            set
            {
                Value = value;
            }
        }

        public bool? CaseInsensitive { get; set; }

        public IFilter ToFilter()
        {
            var type = Enum.Parse<FilterType>(Type);
            if (type == FilterType.Composite)
            {
                var compositeFilter = new CompositeFilter
                {
                    And = And,
                    Type = FilterType.Composite,
                    Filters = Filters?.Select(t => t.ToFilter())?.ToList() ?? new List<IFilter>()
                };
                return compositeFilter;
            }

            object value = Value;
            if (Value is JsonElement jsonElement)
            {
                if (jsonElement.ValueKind == JsonValueKind.String)
                    value = jsonElement.ToString();
                else if (jsonElement.ValueKind == JsonValueKind.Number && jsonElement.TryGetInt64(out var l))
                    value = l;
                else if (jsonElement.ValueKind == JsonValueKind.True)
                    value = true;
                else if (jsonElement.ValueKind == JsonValueKind.False)
                    value = false;
                else if (jsonElement.ValueKind == JsonValueKind.Array)
                    throw new System.Exception("TODO");
                else
                    value = null;
            }

            var simpleFilter = new SimpleFilter
            {
                And = And,
                Type = type,
                Not = Not,
                Path = Path,
                Value = value,
                CaseInsensitive = CaseInsensitive,
            };
            return simpleFilter;
        }
    }
}
