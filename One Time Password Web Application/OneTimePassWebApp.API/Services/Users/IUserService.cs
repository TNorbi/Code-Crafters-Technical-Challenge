using OneTimePassWebApp.API.Data.Responses.Users;

namespace OneTimePassWebApp.API.Services.Users
{
    public interface IUserService
    {
        public Task<AllUsersResponse> getAllUsers();
    }
}
