using EtsyApiSharp.Helpers.Extensions;
using EtsyApiSharp.Models;
using EtsyApiSharp.Models.Common;
using EtsyApiSharp.Models.Filters;
using EtsyApiSharp.Models.Listings.Enums;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;

namespace EtsyApiSharp.Services.ListingManagements
{
    public class EtsyListingManagementService : IEtsyListingManagementService
    {
        private readonly HttpClient _httpClient;
        public EtsyListingManagementService(string clientId)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(Url.AuthUrls.BaseApiUrl) };
            _httpClient.DefaultRequestHeaders.Add("x-api-key", clientId);
        }

        public async Task<ApiResponse<EtsyListResponse<ShopListing>>> FindAllActiveListingsByShopAsync(
            string apiToken,
            long shopId,
            FindAllActiveListingsByShopFilter? filter = null)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);
                UriBuilder baseUri = new UriBuilder($"{_httpClient.BaseAddress}{Url.ListingUrls.FindAllActiveListingsByShop(shopId: shopId)}");

                if (filter is not null)
                {
                    if (filter.Limit is not 25)
                        baseUri.AddQueryParam("limit", filter.Limit.ToString());

                    if (filter.Offset is not 0)
                        baseUri.AddQueryParam("offset", filter.Offset.ToString());

                    if (String.IsNullOrEmpty(filter.Keywords) is false)
                        baseUri.AddQueryParam("keywords", filter.Keywords.ToString());
                }

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, baseUri.Uri);
                var response = _httpClient.SendAsync(request);
                var bodyContent = await response.Result.Content.ReadAsStringAsync();
                var reciepts = JsonSerializer.Deserialize<EtsyListResponse<ShopListing>>(bodyContent);

                var result = new ApiResponse<EtsyListResponse<ShopListing>>
                {
                    ResponseCode = 200,
                    Success = true,
                    Data = reciepts,
                    Message = null
                };

                return result;
            }
            catch (HttpRequestException ex)
            {
                var result = new ApiResponse<EtsyListResponse<ShopListing>>
                {
                    ResponseCode = (int)ex.StatusCode,
                    Success = false,
                    Data = null,
                    Message = $"{ex.Message}"
                };

                return result;
            }
        }

        public async Task<ApiResponse<EtsyListResponse<ShopListing>>> FindAllListingsActiveAsync(
            string apiToken,
            FindAllListingsActiveFilter? filter = null)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);
                UriBuilder baseUri = new UriBuilder($"{_httpClient.BaseAddress}{Url.ListingUrls.FindAllListingsActive()}");

                if (filter is not null)
                {
                    if (filter.Limit is not 25)
                        baseUri.AddQueryParam("limit", filter.Limit.ToString());

                    if (filter.Offset is not 0)
                        baseUri.AddQueryParam("offset", filter.Offset.ToString());

                    if(filter.SortOn is not null)
                        baseUri.AddQueryParam("sort_on", filter.SortOn.ToString());

                    if (filter.SortOrder is not null)
                        baseUri.AddQueryParam("sort_order", filter.SortOrder.ToString());

                    if (filter.MinPrice is not null)
                        baseUri.AddQueryParam("min_price", filter.MinPrice.ToString());

                    if (filter.MaxPrice is not null)
                        baseUri.AddQueryParam("max_price", filter.MaxPrice.ToString());

                    if (filter.Keywords is not null)
                        baseUri.AddQueryParam("keywords", filter.Keywords);

                    if (filter.TaxonomyId is not null)
                        baseUri.AddQueryParam("taxonomy_id", filter.TaxonomyId.ToString());

                    if (filter.ShopLocation is not null)
                        baseUri.AddQueryParam("shop_location", filter.ShopLocation);
                }

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, baseUri.Uri);
                var response = _httpClient.SendAsync(request);
                var bodyContent = await response.Result.Content.ReadAsStringAsync();
                var reciepts = JsonSerializer.Deserialize<EtsyListResponse<ShopListing>>(bodyContent);

                var result = new ApiResponse<EtsyListResponse<ShopListing>>
                {
                    ResponseCode = 200,
                    Success = true,
                    Data = reciepts,
                    Message = null
                };

                return result;
            }
            catch (HttpRequestException ex)
            {
                var result = new ApiResponse<EtsyListResponse<ShopListing>>
                {
                    ResponseCode = (int)ex.StatusCode,
                    Success = false,
                    Data = null,
                    Message = $"{ex.Message}"
                };

                return result;
            }
        }

        public async Task<ApiResponse<EtsyListResponse<ShopListing>>> GetFeaturedListingsByShopAsync(
            string apiToken,
            long shopId,
            GetFeaturedListingsByShopFilter? filter = null)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);
                UriBuilder baseUri = new UriBuilder($"{_httpClient.BaseAddress}{Url.ListingUrls.GetFeaturedListingsByShop(shopId: shopId)}");

                if (filter is not null)
                {
                    if (filter.Limit is not 25)
                        baseUri.AddQueryParam("limit", filter.Limit.ToString());

                    if (filter.Offset is not 0)
                        baseUri.AddQueryParam("offset", filter.Offset.ToString()); 
                }

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, baseUri.Uri);
                var response = _httpClient.SendAsync(request);
                var bodyContent = await response.Result.Content.ReadAsStringAsync();
                var reciepts = JsonSerializer.Deserialize<EtsyListResponse<ShopListing>>(bodyContent);

                var result = new ApiResponse<EtsyListResponse<ShopListing>>
                {
                    ResponseCode = 200,
                    Success = true,
                    Data = reciepts,
                    Message = null
                };

                return result;
            }
            catch (HttpRequestException ex)
            {
                var result = new ApiResponse<EtsyListResponse<ShopListing>>
                {
                    ResponseCode = (int)ex.StatusCode,
                    Success = false,
                    Data = null,
                    Message = $"{ex.Message}"
                };

                return result;
            }
        }

        public async Task<ApiResponse<ShopListingWithAssociations>> GetListingAsync(
            string apiToken,
            long listingId,
            List<ListingInclude>? includes = null)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);
                UriBuilder baseUri = new UriBuilder($"{_httpClient.BaseAddress}{Url.ListingUrls.GetListing(listingId: listingId)}");
                string includesQueryParam = String.Empty;

                if (includes is not null)
                {
                    foreach (var include in includes)
                    {
                        includesQueryParam += $"{include},";
                    }

                    baseUri.AddQueryParam("includes", includesQueryParam);
                }

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, baseUri.Uri);
                var response = _httpClient.SendAsync(request);
                var bodyContent = await response.Result.Content.ReadAsStringAsync();
                var reciepts = JsonSerializer.Deserialize<ShopListingWithAssociations>(bodyContent);

                var result = new ApiResponse<ShopListingWithAssociations>
                {
                    ResponseCode = 200,
                    Success = true,
                    Data = reciepts,
                    Message = null
                };

                return result;
            }
            catch (HttpRequestException ex)
            {
                Debug.WriteLine(ex.Message);
                var result = new ApiResponse<ShopListingWithAssociations>
                {
                    ResponseCode = (int)ex.StatusCode,
                    Success = false,
                    Data = null,
                    Message = $"{ex.Message}"
                };

                return result;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                var result = new ApiResponse<ShopListingWithAssociations>
                {
                    ResponseCode = 500,
                    Success = false,
                    Data = null,
                    Message = $"{ex.Message}"
                };

                return result;
            }
        }

        public Task<ApiResponse<EtsyListResponse<ListingPropertyValue>>> GetListingPropertiesAsync(
            string apiToken,
            long shopId,
            long listingId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<ListingPropertyValue>> GetListingPropertyAsync(
            string apiToken,
            long listingId,
            long propertyId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<EtsyListResponse<ShopListing>>> GetListingsByListingIdsAsync(
            string apiToken,
            List<long> listingIds,
            List<ListingInclude>? includes = null)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<EtsyListResponse<ShopListing>>> GetListingsByShopAsync(
            string apiToken,
            long shopId,
            GetListingsByShopFilter? filter = null)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);
                UriBuilder baseUri = new UriBuilder($"{_httpClient.BaseAddress}{Url.ListingUrls.GetListingsByShop(shopId: shopId)}");

                if (filter is not null)
                {
                    if (filter.Limit is not 25)
                        baseUri.AddQueryParam("limit", filter.Limit.ToString());

                    if (filter.Offset is not 0)
                        baseUri.AddQueryParam("offset", filter.Offset.ToString());

                    if(filter.State is not ListingState.active)
                        baseUri.AddQueryParam("state", filter.State.ToString());

                    if(filter.SortOn is not ListingSortOn.created)
                        baseUri.AddQueryParam("sort_on", filter.SortOn.ToString());

                    if (filter.SortOrder is not ListingSortOrder.desc)
                        baseUri.AddQueryParam("sort_order", filter.SortOrder.ToString());
                }

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, baseUri.Uri);
                var response = _httpClient.SendAsync(request);
                var bodyContent = await response.Result.Content.ReadAsStringAsync();
                var reciepts = JsonSerializer.Deserialize<EtsyListResponse<ShopListing>>(bodyContent);

                var result = new ApiResponse<EtsyListResponse<ShopListing>>
                {
                    ResponseCode = 200,
                    Success = true,
                    Data = reciepts,
                    Message = null
                };

                return result;
            }
            catch (HttpRequestException ex)
            {
                var result = new ApiResponse<EtsyListResponse<ShopListing>>
                {
                    ResponseCode = (int)ex.StatusCode,
                    Success = false,
                    Data = null,
                    Message = $"{ex.Message}"
                };

                return result;
            }
        }

        public async Task<ApiResponse<EtsyListResponse<ShopListing>>> GetListingsByShopReceiptAsync(
            string apiToken,
            long shopId,
            long receiptId,
            GetListingsByShopReceiptFilter? filter = null)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);
                UriBuilder baseUri = new UriBuilder($"{_httpClient.BaseAddress}{Url.ListingUrls.GetListingsByShopReceipt(shopId: shopId, receiptId: receiptId)}");

                if (filter is not null)
                {
                    if (filter.Limit is not 25)
                        baseUri.AddQueryParam("limit", filter.Limit.ToString());

                    if (filter.Offset is not 0)
                        baseUri.AddQueryParam("offset", filter.Offset.ToString());
                }

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, baseUri.Uri);
                var response = _httpClient.SendAsync(request);
                var bodyContent = await response.Result.Content.ReadAsStringAsync();
                var reciepts = JsonSerializer.Deserialize<EtsyListResponse<ShopListing>>(bodyContent);

                var result = new ApiResponse<EtsyListResponse<ShopListing>>
                {
                    ResponseCode = 200,
                    Success = true,
                    Data = reciepts,
                    Message = null
                };

                return result;
            }
            catch (HttpRequestException ex)
            {
                var result = new ApiResponse<EtsyListResponse<ShopListing>>
                {
                    ResponseCode = (int)ex.StatusCode,
                    Success = false,
                    Data = null,
                    Message = $"{ex.Message}"
                };

                return result;
            }
        }

        public Task<ApiResponse<EtsyListResponse<ShopListing>>> GetListingsByShopSectionIdAsync(
            string apiToken,
            long shopId,
            GetListingsByShopSectionIdFilter filter)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<EtsyListResponse<TaxonomyNodeProperty>>> GetPropertiesByTaxonomyIdAsync(
            string apiToken,
            long taxonomyId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<EtsyListResponse<SellerTaxonomyNode>>> GetSellerTaxonomyNodesAsync(
            string apiToken)
        {
            throw new NotImplementedException();
        }
    }
}
