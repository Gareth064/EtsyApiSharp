using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EtsyApiSharp.Helpers.Converters;
/// <summary>
/// Represents Json Nullable Enum Int32 Converter.
/// </summary>

public class JsonNullableEnumInt32Converter<TEnum> : JsonConverter<TEnum>
{
    private readonly bool _isNullable;
    private readonly Type _enumType;
    /// <summary>
    /// Initializes a new instance of the JsonNullableEnumInt32Converter class.
    /// </summary>

    public JsonNullableEnumInt32Converter()
    {
        _isNullable = Nullable.GetUnderlyingType(typeof(TEnum)) != null;

        _enumType = _isNullable ?
            Nullable.GetUnderlyingType(typeof(TEnum))! :
            typeof(TEnum);
    }
    /// <summary>
    /// Executes the Read operation.
    /// </summary>

    public override TEnum Read(ref Utf8JsonReader reader,
        Type typeToConvert, JsonSerializerOptions options)
    {
        int? value = reader.GetInt32();

        if (_isNullable && value is null)
            return default!; //It's a nullable enum, so this returns null. 
        else if (value is null)
            throw new InvalidEnumArgumentException(
                $"A value must be provided for non-nullable enum property of type {typeof(TEnum).FullName}");

        if (!Enum.TryParse(_enumType, value.ToString(), false, out var result)
            && !Enum.TryParse(_enumType, value.ToString(), true, out result))
        {
            throw new JsonException(
                $"Unable to convert \"{value}\" to Enum \"{_enumType}\".");
        }

        return (TEnum)result!;
    }
    /// <summary>
    /// Executes the Write operation.
    /// </summary>

    public override void Write(Utf8JsonWriter writer,
        TEnum value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value?.ToString());
    }
}
