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

        public Task<ApiListResponse<List<ShopListing>>> FindAllActiveListingsByShopAsync(
            string apiToken,
            long shopId,
            FindAllActiveListingsByShopFilter? filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<List<ShopListing>>> FindAllListingsActiveAsync(
            string apiToken,
            FindAllListingsActiveFilter? filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<List<ShopListing>>> GetFeaturedListingsByShopAsync(
            string apiToken,
            long shopId,
            GetFeaturedListingsByShopFilter? filter = null)
        {
            throw new NotImplementedException();
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

        public Task<ApiResponse<List<ListingPropertyValue>>> GetListingPropertiesAsync(
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

        public Task<ApiResponse<List<ShopListing>>> GetListingsByListingIdsAsync(
            string apiToken,
            List<long> listingIds,
            List<ListingInclude>? includes = null)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<List<ShopListing>>> GetListingsByShopAsync(
            string apiToken,
            long shopId,
            GetListingsByShopFilter? filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<List<ShopListing>>> GetListingsByShopReceiptAsync(
            string apiToken,
            long shopId,
            long receiptId,
            GetListingsByShopReceiptFilter? filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<List<ShopListing>>> GetListingsByShopSectionIdAsync(
            string apiToken,
            long shopId,
            GetListingsByShopSectionIdFilter filter)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<List<TaxonomyNodeProperty>>> GetPropertiesByTaxonomyIdAsync(
            string apiToken,
            long taxonomyId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<List<SellerTaxonomyNode>>> GetSellerTaxonomyNodesAsync(
            string apiToken)
        {
            throw new NotImplementedException();
        }
    }
}
