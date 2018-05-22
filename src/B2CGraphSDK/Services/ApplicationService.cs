using System;
using System.Threading.Tasks;

using B2CGraphSDK.Interfaces;

using Microsoft.Extensions.Logging;

namespace B2CGraphSDK.Services
{
    public class ApplicationService : AbstractService, IApplicationService
    {
        public ApplicationService(B2COptions options, ILoggerFactory loggerFactory)
            : base(options, loggerFactory)
        { }

        public async Task<string> GetApplicationsAsync(string query)
        {
            return await SendGraphGetRequest("/applications", query);
        }

        public async Task<string> GetExtensionsAsync(string appObjectId)
        {
            return await SendGraphGetRequest("/applications/" + appObjectId + "/extensionProperties", null);
        }

        public async Task<string> RegisterExtensionAsync(string objectId, string body)
        {
            return await SendGraphPostRequest("/applications/" + objectId + "/extensionProperties", body);
        }

        public async Task<string> UnregisterExtensionAsync(string appObjectId, string extensionObjectId)
        {
            return await SendGraphDeleteRequest("/applications/" + appObjectId + "/extensionProperties/" + extensionObjectId);
        }
    }
}