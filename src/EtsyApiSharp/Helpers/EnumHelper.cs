using EtsyApiSharp.Helpers.Attributes;
using System.Reflection;

namespace EtsyApiSharp.Helpers;
/// <summary>
/// Represents Enum Helper.
/// </summary>

public static class EnumHelper
{
    /// <summary>
    /// Executes the Get String Value operation.
    /// </summary>
    public static string GetStringValue(Enum value)
    {
        string? output = null;
        Type type = value.GetType();
        FieldInfo fi = type.GetField(value.ToString())!;
        EnumValue[]? attrs = fi.GetCustomAttributes(typeof(EnumValue), false) as EnumValue[];
        if (attrs!.Length > 0)
        {
            output = attrs[0].Value;
        }
        return output!;
    }
}
