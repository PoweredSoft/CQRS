using System;

namespace PoweredSoft.CQRS.GraphQL.DynamicQuery
{
    public abstract class GraphQLVariant
    {

        protected virtual string ResolveTypeName(object value)
        {
            if (value != null)
            {
                if (value is int)
                    return "int";
                if (value is long)
                    return "long";
                if (value is string)
                    return "string";
                if (value is bool)
                    return "boolean";
                if (value is decimal)
                    return "decimal";
                if (value is DateTime)
                    return "datetime";
            }

            return null;
        }

        public string GetTypeName()
        {
            var value = GetRawObjectValue();
            return ResolveTypeName(value);
        }

        public virtual void SetVariant(object raw)
        {
            ClearVariant();
            if (raw != null)
            {
                if (raw is int rawInt)
                    IntValue = rawInt;
                if (raw is long rawLong)
                    LongValue = rawLong;
                if (raw is string rawStr)
                    StringValue = rawStr;
                if (raw is bool rawBool)
                    BooleanValue = rawBool;
                if (raw is decimal rawDec)
                    DecimalValue = rawDec;
                if (raw is DateTime rawDt)
                    DateTimeValue = rawDt;
            }
        }

        public virtual object GetRawObjectValue()
        {
            if (IntValue != null && IntValue is int)
                return IntValue;
            if (LongValue != null && LongValue is long)
                return LongValue;
            if (StringValue != null && StringValue is string)
                return StringValue;
            if (BooleanValue != null && BooleanValue is bool)
                return BooleanValue;
            if (DecimalValue != null && DecimalValue is decimal)
                return DecimalValue;
            if (DateTimeValue != null && DateTimeValue is DateTime)
                return DateTimeValue;

            return null;
        }

        public int? IntValue { get; set; }
        public long? LongValue { get; set; }
        public string StringValue { get; set; }
        public decimal? DecimalValue { get; set; }
        public DateTime? DateTimeValue { get; set; }
        public bool? BooleanValue { get; set; }

        public virtual void ClearVariant()
        {
            this.IntValue = null;
            this.LongValue = null;
            this.StringValue = null;
            this.DecimalValue = null;
            this.DateTimeValue = null;
            this.BooleanValue = null;
        }
    }
}
