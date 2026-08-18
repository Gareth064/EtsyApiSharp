namespace EtsyApiSharp.Helpers.Extensions;
/// <summary>
/// Represents Uri Builder Extension.
/// </summary>

public static class UriBuilderExtension
{
    /// <summary>
    /// Executes the Add Query Param operation.
    /// </summary>
    public static UriBuilder AddQueryParam(this UriBuilder builder, string key, string value)
    {
        var queryToAppend = $"{key}={value}";

        if (builder.Query != null && builder.Query.Length > 1)
            builder.Query = builder.Query.Substring(1) + "&" + queryToAppend;
        else
            builder.Query = queryToAppend;

        return builder;
    }
}
