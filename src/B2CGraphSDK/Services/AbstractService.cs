using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

using Newtonsoft.Json;

namespace B2CGraphSDK.Services
{
    public abstract class AbstractService
    {
        protected AbstractService(B2COptions options, ILoggerFactory loggerFactory)
        {
            ClientId = options.ClientId;
            ClientSecret = options.ClientSecret;
            Tenant = options.Tenant;

            _authContext = new AuthenticationContext(options.Authority);
            _credential = new ClientCredential(ClientId, ClientSecret);
            _logger = loggerFactory.CreateLogger($"{nameof(AbstractService)}");
        }

        protected AuthenticationContext _authContext;

        protected ClientCredential _credential;

        protected readonly ILogger _logger;

        protected async Task<string> SendGraphDeleteRequest(string api)
        {
            // NOTE: This client uses ADAL v2, not ADAL v4
            var result = await _authContext.AcquireTokenAsync(Globals.GraphResourceId, _credential);
            var http = new HttpClient();
            var url = Globals.GraphEndpoint + Tenant + api + "?" + Globals.GraphVersion;
            var request = new HttpRequestMessage(HttpMethod.Delete, url);

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);

            var response = await http.SendAsync(request);

            _logger.LogDebug($"{nameof(AbstractService)}::SendGraphDeleteRequest - DELETE " + url);
            _logger.LogDebug($"{nameof(AbstractService)}::SendGraphDeleteRequest - Authorization: Bearer " + result.AccessToken.Substring(0, 80) + "...");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                var formatted = JsonConvert.DeserializeObject(error);

                throw new WebException("Error Calling the Graph API: \n" + JsonConvert.SerializeObject(formatted, Formatting.Indented));
            }

            _logger.LogDebug($"{nameof(AbstractService)}::SendGraphDeleteRequest - {(int)response.StatusCode} : {response.ReasonPhrase}");

            return await response.Content.ReadAsStringAsync();
        }

        protected async Task<string> SendGraphGetRequest(string api, string query)
        {
            // First, use ADAL to acquire a token using the app's identity (the credential)
            // The first parameter is the resource we want an access_token for; in this case, the Graph API.
            var result = await _authContext.AcquireTokenAsync("https://graph.windows.net", _credential);
            // For B2C user managment, be sure to use the 1.6 Graph API version.
            var http = new HttpClient();
            var url = "https://graph.windows.net/" + Tenant + api + "?" + Globals.GraphVersion;

            if (!string.IsNullOrEmpty(query))
            {
                url += "&" + query;
            }

            _logger.LogDebug($"{nameof(AbstractService)}::SendGraphGetRequest - GET " + url);
            _logger.LogDebug($"{nameof(AbstractService)}::SendGraphGetRequest - Authorization: Bearer " + result.AccessToken.Substring(0, 80) + "...");

            // Append the access token for the Graph API to the Authorization header of the request, using the Bearer scheme.
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);

            var response = await http.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                var formatted = JsonConvert.DeserializeObject(error);

                throw new WebException("Error Calling the Graph API: \n" + JsonConvert.SerializeObject(formatted, Formatting.Indented));
            }

            _logger.LogDebug($"{nameof(AbstractService)}::SendGraphGetRequest - {(int)response.StatusCode} : {response.ReasonPhrase}");

            return await response.Content.ReadAsStringAsync();
        }

        protected async Task<string> SendGraphPatchRequest(string api, string json)
        {
            // NOTE: This client uses ADAL v2, not ADAL v4
            var result = await _authContext.AcquireTokenAsync(Globals.GraphResourceId, _credential);
            var http = new HttpClient();
            var url = Globals.GraphEndpoint + Tenant + api + "?" + Globals.GraphVersion;

            _logger.LogDebug($"{nameof(AbstractService)}::SendGraphPatchRequest - PATCH " + url);
            _logger.LogDebug($"{nameof(AbstractService)}::SendGraphPatchRequest - Authorization: Bearer " + result.AccessToken.Substring(0, 80) + "...");
            _logger.LogDebug($"{nameof(AbstractService)}::SendGraphPatchRequest - Content-Type: application/json");
            _logger.LogDebug(json);

            var request = new HttpRequestMessage(new HttpMethod("PATCH"), url);

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await http.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                var formatted = JsonConvert.DeserializeObject(error);

                throw new WebException("Error Calling the Graph API: \n" + JsonConvert.SerializeObject(formatted, Formatting.Indented));
            }

            _logger.LogDebug($"{nameof(AbstractService)}::SendGraphPatchRequest - {(int)response.StatusCode} : {response.ReasonPhrase}");

            return await response.Content.ReadAsStringAsync();
        }

        protected async Task<string> SendGraphPostRequest(string api, string json)
        {
            // NOTE: This client uses ADAL v2, not ADAL v4
            var result = await _authContext.AcquireTokenAsync(Globals.GraphResourceId, _credential);
            var http = new HttpClient();
            var url = Globals.GraphEndpoint + Tenant + api + "?" + Globals.GraphVersion;

            _logger.LogDebug($"{nameof(AbstractService)}::SendGraphPostRequest - POST " + url);
            _logger.LogDebug($"{nameof(AbstractService)}::SendGraphPostRequest - Authorization: Bearer " + result.AccessToken.Substring(0, 80) + "...");
            _logger.LogDebug($"{nameof(AbstractService)}::SendGraphPostRequest - Content-Type: application/json");
            _logger.LogDebug(json);

            var request = new HttpRequestMessage(HttpMethod.Post, url);

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await http.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                var formatted = JsonConvert.DeserializeObject(error);

                throw new WebException("Error Calling the Graph API: \n" + JsonConvert.SerializeObject(formatted, Formatting.Indented));
            }

            _logger.LogDebug($"{nameof(AbstractService)}::SendGraphPostRequest - {(int)response.StatusCode} : {response.ReasonPhrase}");

            return await response.Content.ReadAsStringAsync();
        }

        protected string ClientId { get; }

        protected string ClientSecret { get; }

        protected string Tenant { get; }
    }
}