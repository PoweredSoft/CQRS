using Newtonsoft.Json;

namespace PoweredSoft.CQRS.GraphQL.DynamicQuery
{
    public class GraphQLVariantResult : GraphQLVariant
    {
        public GraphQLVariantResult()
        {

        }

        public GraphQLVariantResult(object raw)
        {
            SetVariant(raw);
        }

        protected override string ResolveTypeName(object value)
        {
            var valueType = base.ResolveTypeName(value);
            if (value != null && valueType == null)
                return "json";

            return valueType;
        }

        public override object GetRawObjectValue()
        {
            if (jsonValue != null)
                return jsonValue;

            return base.GetRawObjectValue();
        }

        private object jsonValue = null;

        public string Json
        {
            get
            {
                if (jsonValue != null)
                    return JsonConvert.SerializeObject(jsonValue, new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });

                return null;
            }
            set
            {
                jsonValue = JsonConvert.DeserializeObject(value);
            }
        }

        public override void ClearVariant()
        {
            base.ClearVariant();
            this.jsonValue = null;
        }
    }
}