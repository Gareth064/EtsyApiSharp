using Microsoft.JSInterop;
using System.Text.Json;

namespace BlazorTestApp.Data;

/// <summary>
/// Holds the shop list for one browser workspace and persists it in local storage.
/// </summary>
public sealed class ShopWorkspaceState
{
    private const string StorageKey = "etsy-api-workspace.shops.v1";
    private List<RuntimeShop> shops = [];

    public event Action? Changed;

    public IReadOnlyList<RuntimeShop> Shops => shops;

    public async Task LoadAsync(IJSRuntime jsRuntime)
    {
        var json = await jsRuntime.InvokeAsync<string?>("localStorage.getItem", StorageKey);
        if (string.IsNullOrWhiteSpace(json))
            return;

        try
        {
            var storedShops = JsonSerializer.Deserialize<List<RuntimeShop>>(json);
            if (storedShops is null)
                return;

            shops = storedShops
                .Where(shop => shop.Id > 0 && !string.IsNullOrWhiteSpace(shop.Name))
                .GroupBy(shop => shop.Id)
                .Select(group => new RuntimeShop(group.Key, group.First().Name.Trim()))
                .ToList();
        }
        catch (JsonException)
        {
            shops = [];
        }

        Changed?.Invoke();
    }

    public async Task AddShopAsync(IJSRuntime jsRuntime, long shopId, string shopName)
    {
        if (shopId <= 0)
            throw new ArgumentOutOfRangeException(nameof(shopId), "A positive shop ID is required.");

        if (string.IsNullOrWhiteSpace(shopName))
            throw new ArgumentException("A shop name is required.", nameof(shopName));

        if (shops.Any(shop => shop.Id == shopId))
            throw new ArgumentException("That shop ID is already in your workspace.", nameof(shopId));

        shops.Add(new RuntimeShop(shopId, shopName.Trim()));
        await SaveAsync(jsRuntime);
        Changed?.Invoke();
    }

    public async Task RemoveShopAsync(IJSRuntime jsRuntime, long shopId)
    {
        shops.RemoveAll(shop => shop.Id == shopId);
        await SaveAsync(jsRuntime);
        Changed?.Invoke();
    }

    private Task SaveAsync(IJSRuntime jsRuntime) => jsRuntime.InvokeVoidAsync(
        "localStorage.setItem",
        StorageKey,
        JsonSerializer.Serialize(shops)).AsTask();
}

public sealed record RuntimeShop(long Id, string Name);
