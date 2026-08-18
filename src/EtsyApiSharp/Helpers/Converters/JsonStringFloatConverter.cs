using System.Text.Json;
using System.Text.Json.Serialization;

namespace EtsyApiSharp.Helpers.Converters;
/// <summary>
/// Represents Json String Float Converter.
/// </summary>

public class JsonStringFloatConverter : JsonConverter<float>
{
    /// <summary>
    /// Executes the Read operation.
    /// </summary>
    public override float Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options) =>
            float.Parse(reader.GetString()!);
    /// <summary>
    /// Executes the Write operation.
    /// </summary>

    public override void Write(
        Utf8JsonWriter writer,
        float floatValue,
        JsonSerializerOptions options) =>
            writer.WriteStringValue(floatValue.ToString());
}
