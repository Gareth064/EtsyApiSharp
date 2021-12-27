namespace EtsyApiSharp.Helpers.Extensions
{
    public static class UriBuilderExtension
    {
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
}
