using BlazorTestApp.Data;
using EtsyApiSharp.Models;
using EtsyApiSharp.Services.Auths;
using EtsyApiSharp.Services.ListingManagements;
using EtsyApiSharp.Services.PaymentManagements;
using EtsyApiSharp.Services.ReceiptManagements;
using EtsyApiSharp.Services.ReviewManagements;
using EtsyApiSharp.Services.ShopManagements;
using EtsyApiSharp.Services.ShippingManagements;

var builder = WebApplication.CreateBuilder(args);
var scopes = new[] { Scope.shops_r, Scope.shops_w, Scope.cart_r, Scope.listings_w, Scope.listings_r, Scope.email_r, Scope.transactions_r };

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<RuntimeEtsySettings>();
builder.Services.AddScoped<ShopWorkspaceState>();
builder.Services.AddHttpClient();

builder.Services.AddHttpClient(EtsyAuthService.HttpClientName);
builder.Services.AddScoped<IEtsyAuthService>(provider =>
{
    var settings = provider.GetRequiredService<RuntimeEtsySettings>();
    return new EtsyAuthService(provider.GetRequiredService<IHttpClientFactory>(), settings.ClientIdForService, settings.RedirectUri, scopes);
});

builder.Services.AddHttpClient(EtsyReceiptManagementService.HttpClientName);
builder.Services.AddTransient<IEtsyReceiptManagementService>(provider =>
{
    var settings = provider.GetRequiredService<RuntimeEtsySettings>();
    return new EtsyReceiptManagementService(provider.GetRequiredService<IHttpClientFactory>(), settings.ClientIdForService, settings.SharedSecretForService);
});

builder.Services.AddHttpClient(EtsyPaymentManagementService.HttpClientName);
builder.Services.AddTransient<IEtsyPaymentManagementService>(provider =>
{
    var settings = provider.GetRequiredService<RuntimeEtsySettings>();
    return new EtsyPaymentManagementService(provider.GetRequiredService<IHttpClientFactory>(), settings.ClientIdForService, settings.SharedSecretForService);
});

builder.Services.AddHttpClient(EtsyReviewManagementService.HttpClientName);
builder.Services.AddTransient<IEtsyReviewManagementService>(provider =>
{
    var settings = provider.GetRequiredService<RuntimeEtsySettings>();
    return new EtsyReviewManagementService(provider.GetRequiredService<IHttpClientFactory>(), settings.ClientIdForService, settings.SharedSecretForService);
});

builder.Services.AddHttpClient(EtsyListingManagementService.HttpClientName);
builder.Services.AddTransient<IEtsyListingManagementService>(provider =>
{
    var settings = provider.GetRequiredService<RuntimeEtsySettings>();
    return new EtsyListingManagementService(provider.GetRequiredService<IHttpClientFactory>(), settings.ClientIdForService, settings.SharedSecretForService);
});

builder.Services.AddHttpClient(EtsyShopManagementService.HttpClientName);
builder.Services.AddScoped<IEtsyShopManagementService>(provider =>
{
    var settings = provider.GetRequiredService<RuntimeEtsySettings>();
    return new EtsyShopManagementService(provider.GetRequiredService<IHttpClientFactory>(), settings.ClientIdForService, settings.SharedSecretForService);
});

builder.Services.AddHttpClient(EtsyShippingManagementService.HttpClientName);
builder.Services.AddTransient<IEtsyShippingManagementService>(provider =>
{
    var settings = provider.GetRequiredService<RuntimeEtsySettings>();
    return new EtsyShippingManagementService(provider.GetRequiredService<IHttpClientFactory>(), settings.ClientIdForService, settings.SharedSecretForService);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();
