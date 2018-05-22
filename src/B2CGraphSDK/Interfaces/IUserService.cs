using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using B2CGraphSDK.Models;

namespace B2CGraphSDK.Interfaces
{
    public interface IUserService
    {
        Task<bool> CreateAsync(UserModel model);

        Task<bool> DeleteAsync(string objectId);

        Task<List<UserModel>> GetAllAsync(string query);

        Task<UserModel> GetByIdAsync(string objectId);

        Task<bool> UpdateAsync(string objectId, UserModel model);
    }
}