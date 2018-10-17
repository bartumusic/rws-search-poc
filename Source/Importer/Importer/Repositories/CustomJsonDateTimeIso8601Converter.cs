using System;
using Raven.Abstractions.Json;
using Raven.Imports.Newtonsoft.Json;

namespace Importer.Repositories
{
    public class CustomJsonDateTimeIso8601Converter : JsonDateTimeISO8601Converter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is DateTime)
            {
                var dateTime = (DateTime)value;
                if (dateTime.Kind != DateTimeKind.Utc)
                {
                    throw new FormatException(string.Format("Date {0} is not of UTC format.", dateTime));
                }
            }

            base.WriteJson(writer, value, serializer);
        }
    }
}
