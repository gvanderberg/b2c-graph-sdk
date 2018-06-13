using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using B2CGraphSDK.Interfaces;
using B2CGraphSDK.Models;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

namespace B2CGraphSDK.Services
{
    public class UserService : AbstractService, IUserService
    {
        public UserService(B2COptions options, ILoggerFactory loggerFactory)
            : base(options, loggerFactory)
        { }

        public async Task<bool> CreateAsync(UserModel model)
        {
            var json = JsonConvert.SerializeObject(model);
            var response = await SendGraphPostRequest("/users", json);

            return string.IsNullOrEmpty(response);
        }

        public async Task<bool> DeleteAsync(string objectId)
        {
            var response = await SendGraphDeleteRequest("/users/" + objectId);

            return string.IsNullOrEmpty(response);
        }

        public async Task<List<UserModel>> GetAllAsync(string query = "")
        {
            var response = await SendGraphGetRequest("/users", query);
            var result = JsonConvert.DeserializeObject<ServiceResult<List<UserModel>>>(response);

            return result?.Value;
        }

        public async Task<UserModel> GetByIdAsync(string objectId)
        {
            var json = await SendGraphGetRequest("/users/" + objectId, null);

            return JsonConvert.DeserializeObject<UserModel>(json);
        }

        public async Task<bool> UpdateAsync(string objectId, UserModel model)
        {
            var json = JsonConvert.SerializeObject(model);
            var response = await SendGraphPatchRequest("/users/" + objectId, json);

            return string.IsNullOrEmpty(response);
        }
    }
}