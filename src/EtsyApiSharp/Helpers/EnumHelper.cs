using EtsyApiSharp.Helpers.Attributes;
using System.Reflection;

namespace EtsyApiSharp.Helpers;

public static class EnumHelper
{
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
