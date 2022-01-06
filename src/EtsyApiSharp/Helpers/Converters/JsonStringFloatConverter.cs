using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EtsyApiSharp.Helpers.Converters
{
    public class JsonStringFloatConverter : JsonConverter<float>
    {
        public override float Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) =>
                float.Parse(reader.GetString());

        public override void Write(
            Utf8JsonWriter writer,
            float floatValue,
            JsonSerializerOptions options) =>
                writer.WriteStringValue(floatValue.ToString());
    }
}
