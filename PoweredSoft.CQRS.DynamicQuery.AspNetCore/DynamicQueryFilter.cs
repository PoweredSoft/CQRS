using PoweredSoft.DynamicQuery;
using PoweredSoft.DynamicQuery.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace PoweredSoft.CQRS.DynamicQuery.AspNetCore
{
    public class DynamicQueryFilter
    {
        public List<DynamicQueryFilter> Filters { get; set; }
        public bool? And { get; set; }
        public FilterType Type { get; set; }
        public bool? Not { get; set; }
        public string Path { get; set; }
        public object Value { get; set; }

        public IFilter ToFilter()
        {
            if (Type == FilterType.Composite)
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
                Type = Type,
                Not = Not,
                Path = Path,
                Value = value
            };
            return simpleFilter;
        }
    }
}
