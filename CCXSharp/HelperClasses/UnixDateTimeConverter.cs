using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CCXSharp.HelperClasses
{
    public class UnixDateTimeConverter : DateTimeConverterBase
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            long ticks;
            if (value is DateTime)
            {
                var epoc = new DateTime(1970, 1, 1);
                var delta = ((DateTime)value) - epoc;
                if (delta.TotalSeconds < 0)
                {
                    throw new ArgumentOutOfRangeException("Unix epoc starts January 1st, 1970");
                }
                ticks = (long)delta.TotalSeconds;
            }
            else
            {
                throw new Exception("Expected date object value.");
            }
            writer.WriteValue(ticks);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
                                        JsonSerializer serializer)
        {
            DateTime dt;
            Int64 ticks;
            switch (reader.TokenType)
            {
                case JsonToken.String:
                    ticks = long.Parse((string)reader.Value);
                    dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                    dt = dt.AddMilliseconds(ticks / 1000);
                    break;
                case JsonToken.Integer:
                    ticks = (long)reader.Value;
                    dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                    dt = dt.AddSeconds(ticks);
                    break;
                default:
                    throw new Exception("Invalid token type: Cannot convert to DateTime");
            }
            return dt;
        }
    }
}
