﻿namespace OneTimePassWebApp.API.Repositories.Users
{
    public interface IUserRepository
    {
        public Task<IEnumerable<Data.Models.Users>> getAllUsers();

        public Task<Data.Models.Users?> getUserByUserId(int userId);
        public Task<IEnumerable<Data.Models.Users>?> getUserByUsername(string userName);
    }
}
