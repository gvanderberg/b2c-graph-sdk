using System;
using System.Threading.Tasks;

namespace B2CGraphSDK.Interfaces
{
    public interface IApplicationService
    {
        Task<string> GetApplicationsAsync(string query);

        Task<string> GetExtensionsAsync(string appObjectId);

        Task<string> RegisterExtensionAsync(string objectId, string body);

        Task<string> UnregisterExtensionAsync(string appObjectId, string extensionObjectId);
    }
}