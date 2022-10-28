namespace OneTimePassWebApp.API.Repositories.Users
{
    public interface IUserRepository
    {
        public Task<IEnumerable<Data.Models.Users>> getAllUsers();

        public Task<Data.Models.Users?> getUserByUserId(int userId);
        public Task<Data.Models.Users?> getUserByUsername(string userName);

        public Task<Data.Models.Users?> registerNewUser(Data.Models.Users newUser);

        public Task<Data.Models.DTOs.Users.UserLoginDTO?> loginUser(Data.Models.Users user);
    }
}
