namespace EtsyApiSharp.Helpers.Attributes;

public class EnumValue : Attribute
{
    private string _value;
    public EnumValue(string value)
    {
        _value = value;
    }
    public string Value
    {
        get { return _value; }
    }
}
