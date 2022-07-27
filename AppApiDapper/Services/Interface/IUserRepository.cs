﻿using WebData.Entities;
using WebData.Models;

namespace AppApiDapper.Services.Interface
{
    public interface IUserRepository : IGenericRepository<UserModel>
    {
        Task<IEnumerable<UserModel>> GetAll(int pageIndex, int pageSize);
    }
}
