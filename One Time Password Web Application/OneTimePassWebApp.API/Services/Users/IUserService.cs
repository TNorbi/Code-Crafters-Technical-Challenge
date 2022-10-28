using OneTimePassWebApp.API.Data.Responses.Users;

namespace OneTimePassWebApp.API.Services.Users
{
    public interface IUserService
    {
        public Task<AllUsersResponse> getAllUsers();

        public Task<UserResponse> getUserById(int userId);
        public Task<AllUsersResponse> getUserByUsername(string userName);
    }
}
