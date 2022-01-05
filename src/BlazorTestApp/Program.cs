using BlazorTestApp.Data;
using EtsyApiSharp;
using EtsyApiSharp.Models;

List<Scope> scopes = new List<Scope> { Scope.shops_r, Scope.shops_w, Scope.cart_r, Scope.listings_w, Scope.listings_r, Scope.email_r, Scope.transactions_r };

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddHttpClient();
builder.Services.AddEtsyAuthServiceSingleton("gss0u2qxjyv991mbjbwn2208", "https://localhost:5001/secret/callback", scopes);
builder.Services.AddEtsyReceiptManagementServiceTransient("gss0u2qxjyv991mbjbwn2208");
builder.Services.AddEtsyListingManagementServiceTransient("gss0u2qxjyv991mbjbwn2208");


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
