using OneTimePassWebApp.API.Data.Models;
using OneTimePassWebApp.API.Data.Requests.OTP;

namespace OneTimePassWebApp.API.Repositories.Users
{
    public interface IUserRepository
    {
        public Task<IEnumerable<Data.Models.Users>> getAllUsers();

        public Task<Data.Models.Users?> getUserByUserId(int userId);
        public Task<Data.Models.Users?> getUserByUsername(string userName);

        public Task<Data.Models.Users?> registerNewUser(Data.Models.Users newUser);

        public Task<Data.Models.DTOs.Users.UserLoginDTO?> loginUser(Data.Models.Users user);

        public Task<String?> generateOTP(int userID);

        public Task<OTPCheckers?> verifyOTP(OTPVerifyRequest request);
    }
}
