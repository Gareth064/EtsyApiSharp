namespace EtsyApiSharp.Helpers.Attributes;
/// <summary>
/// Represents Enum Value.
/// </summary>

public class EnumValue : Attribute
{
    private string _value;
    /// <summary>
    /// Initializes a new instance of the EnumValue class.
    /// </summary>
    public EnumValue(string value)
    {
        _value = value;
    }
    /// <summary>
    /// Gets or sets the Value.
    /// </summary>
    public string Value
    {
        get { return _value; }
    }
}
