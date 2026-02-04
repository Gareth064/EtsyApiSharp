using EtsyApiSharp.Helpers;
using EtsyApiSharp.Helpers.Extensions;
using EtsyApiSharp.Infrastructure;
using EtsyApiSharp.Models;
using EtsyApiSharp.Models.Common;
using EtsyApiSharp.Models.Filters;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace EtsyApiSharp.Services.ReceiptManagements;

public class EtsyReceiptManagementService : IEtsyReceiptManagementService
{
    private static IHttpClientFactory httpClientFactory = new DefaultHttpClientFactory();
    private readonly string clientId;
    private readonly string sharedSecret;
    private readonly string apiKey;

    public EtsyReceiptManagementService(string clientId, string sharedSecret)
    {
        this.clientId = clientId;
        this.sharedSecret = sharedSecret;
        this.apiKey = $"{clientId}:{sharedSecret}";
    }

    public async Task<ApiResponse<ShopReceipt>> CreateReceiptShipmentAsync(
        string apiToken,
        long shopId,
        ShopReceiptShipment shopReceiptShipment)
    {
        throw new NotImplementedException();
    }

    public async Task<ApiResponse<ShopReceipt>> GetShopReceiptAsync(
        string apiToken,
        long shopId,
        long receiptId)
    {
        try
        {
            var httpClient = httpClientFactory.CreateClient();
            UriBuilder baseUri = new($"{Url.BaseUrls.BaseApiUrl}{Url.ReceiptUrls.GetShopReceipt(shopId: shopId, receiptId: receiptId)}");
            HttpRequestMessage request = new(HttpMethod.Get, baseUri.Uri);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);
            request.Headers.Add("x-api-key", apiKey);
            var response = await httpClient.SendAsync(request);
            var result = await EtsyResponseParser.ParseResponseOfSingle<ShopReceipt>(response);

            return result;
        }
        catch (HttpRequestException ex)
        {
            var result = new ApiResponse<ShopReceipt>
            {
                ResponseCode = (int)ex.StatusCode!,
                Success = false,
                Data = null,
                Message = $"{ex.Message}"
            };

            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            var result = new ApiResponse<ShopReceipt>
            {
                ResponseCode = 500,
                Success = false,
                Data = null,
                Message = $"{ex.Message}"
            };

            return result;
        }
    }

    public async Task<ApiResponse<EtsyListResponse<ShopReceipt>>> GetShopReceiptsAsync(
        string apiToken,
        long shopId,
        GetShopReceiptsFilter filter)
    {
        try
        {
            UriBuilder baseUri = new($"{Url.BaseUrls.BaseApiUrl}{Url.ReceiptUrls.GetShopReceipts(shopId: shopId)}");

            if (filter.Limit is not 25)
                baseUri.AddQueryParam("limit", filter.Limit.ToString());

            if (filter.Offset is not 0)
                baseUri.AddQueryParam("offset", filter.Offset.ToString());

            if (filter.MinCreated is not null)
                baseUri.AddQueryParam("min_created", filter.MinCreated.ToString()!);

            if (filter.MaxLastModified is not null)
                baseUri.AddQueryParam("max_created", filter.MaxCreated.ToString()!);

            if (filter.MinLastModified is not null)
                baseUri.AddQueryParam("min_last_modified", filter.MinLastModified.ToString()!);

            if (filter.MaxLastModified is not null)
                baseUri.AddQueryParam("max_last_modified", filter.MaxLastModified.ToString()!);

            if (filter.WasPaid is not null)
                baseUri.AddQueryParam("was_paid", filter.WasPaid.ToString()!);

            if (filter.WasCancelled is not null)
                baseUri.AddQueryParam("was_canceled", filter.WasCancelled.ToString()!);

            if (filter.WasShipped is not null)
                baseUri.AddQueryParam("was_shipped", filter.WasShipped.ToString()!);


            if (filter.WasDelivered is not null)
                baseUri.AddQueryParam("was_delivered", filter.WasDelivered.ToString()!);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, baseUri.Uri);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);
            request.Headers.Add("x-api-key", apiKey);

            var httpClient = httpClientFactory.CreateClient();
            var response = await httpClient.SendAsync(request);

            var result = await EtsyResponseParser.ParseResponseOfList<ShopReceipt>(response);

            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            var result = new ApiResponse<EtsyListResponse<ShopReceipt>>
            {
                ResponseCode = 500,
                Success = false,
                Data = null,
                Message = $"{ex.Message}"
            };

            return result;
        }
    }

    public async Task<ApiResponse<ShopReceiptTransaction>> GetShopReceiptTransactionAsync(
        string apiToken,
        long shopId,
        long transactionId)
    {
        try
        {
            UriBuilder baseUri = new($"{Url.BaseUrls.BaseApiUrl}{Url.ReceiptUrls.GetShopReceiptTransaction(shopId: shopId, transactionId: transactionId)}");
            HttpRequestMessage request = new(HttpMethod.Get, baseUri.Uri);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);
            request.Headers.Add("x-api-key", apiKey);

            var httpClient = httpClientFactory.CreateClient();
            var response = await httpClient.SendAsync(request);
            var result = await EtsyResponseParser.ParseResponseOfSingle<ShopReceiptTransaction>(response);

            return result;
        }
        catch (HttpRequestException ex)
        {
            var result = new ApiResponse<ShopReceiptTransaction>
            {
                ResponseCode = (int)ex.StatusCode!,
                Success = false,
                Data = null,
                Message = $"{ex.Message}"
            };

            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            var result = new ApiResponse<ShopReceiptTransaction>
            {
                ResponseCode = 500,
                Success = false,
                Data = null,
                Message = $"{ex.Message}"
            };

            return result;
        }
    }

    public async Task<ApiResponse<EtsyListResponse<ShopReceiptTransaction>>> GetShopReceiptTransactionsByListingAsync(
        string apiToken,
        long shopId,
        long listingId,
        GetShopReceiptTransactionsByListingFilter filter)
    {
        try
        {
            UriBuilder baseUri = new($"{Url.BaseUrls.BaseApiUrl}{Url.ReceiptUrls.GetShopReceiptTransactionsByListing(shopId: shopId, listingId: listingId)}");
            HttpRequestMessage request = new(HttpMethod.Get, baseUri.Uri);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);
            request.Headers.Add("x-api-key", apiKey);
            var httpClient = httpClientFactory.CreateClient();
            var response = await httpClient.SendAsync(request);
            var result = await EtsyResponseParser.ParseResponseOfList<ShopReceiptTransaction>(response);

            return result;
        }
        catch (HttpRequestException ex)
        {
            var result = new ApiResponse<EtsyListResponse<ShopReceiptTransaction>>
            {
                ResponseCode = (int)ex.StatusCode!,
                Success = false,
                Data = null,
                Message = $"{ex.Message}"
            };

            return result;
        }
    }

    public async Task<ApiResponse<EtsyListResponse<ShopReceiptTransaction>>> GetShopReceiptTransactionsByReceiptAsync(
        string apiToken,
        long shopId,
        long receiptId)
    {
        try
        {
            UriBuilder baseUri = new($"{Url.BaseUrls.BaseApiUrl}{Url.ReceiptUrls.GetShopReceiptTransactionsByReceipt(shopId: shopId, receiptId: receiptId)}");
            HttpRequestMessage request = new(HttpMethod.Get, baseUri.Uri);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);
            request.Headers.Add("x-api-key", apiKey);

            var httpClient = httpClientFactory.CreateClient();
            var response = await httpClient.SendAsync(request);
            var result = await EtsyResponseParser.ParseResponseOfList<ShopReceiptTransaction>(response);

            return result;
        }
        catch (HttpRequestException ex)
        {
            var result = new ApiResponse<EtsyListResponse<ShopReceiptTransaction>>
            {
                ResponseCode = (int)ex.StatusCode!,
                Success = false,
                Data = null,
                Message = $"{ex.Message}"
            };

            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            var result = new ApiResponse<EtsyListResponse<ShopReceiptTransaction>>
            {
                ResponseCode = 500,
                Success = false,
                Data = null,
                Message = $"{ex.Message}"
            };

            return result;
        }
    }

    public async Task<ApiResponse<EtsyListResponse<ShopReceiptTransaction>>> GetShopReceiptTransactionsByShopAsync(
        string apiToken,
        long shopId,
        GetShopReceiptTransactionsByShopFilter filter)
    {
        try
        {
            UriBuilder baseUri = new($"{Url.BaseUrls.BaseApiUrl}{Url.ReceiptUrls.GetShopReceiptTransactionsByShop(shopId: shopId)}");

            if (filter.Limit is not 25)
                baseUri.AddQueryParam("limit", filter.Limit.ToString());

            if (filter.Offset is not 0)
                baseUri.AddQueryParam("offset", filter.Offset.ToString());

            HttpRequestMessage request = new(HttpMethod.Get, baseUri.Uri);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);
            request.Headers.Add("x-api-key", apiKey);

            var httpClient = httpClientFactory.CreateClient();
            var response = await httpClient.SendAsync(request);
            var result = await EtsyResponseParser.ParseResponseOfList<ShopReceiptTransaction>(response);

            return result;
        }
        catch (HttpRequestException ex)
        {
            var result = new ApiResponse<EtsyListResponse<ShopReceiptTransaction>>
            {
                ResponseCode = (int)ex.StatusCode!,
                Success = false,
                Data = null,
                Message = $"{ex.Message}"
            };

            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            var result = new ApiResponse<EtsyListResponse<ShopReceiptTransaction>>
            {
                ResponseCode = 500,
                Success = false,
                Data = null,
                Message = $"{ex.Message}"
            };

            return result;
        }
    }

}
