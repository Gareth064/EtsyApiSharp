using EtsyApiSharp.Models;
using EtsyApiSharp.Models.Common;

namespace EtsyApiSharp.Services.UserManagements;
public interface IEtsyUserManagementService
{
    Task<ApiResponse<User>> GetUserAsync(string apiTpken, long id);
}
