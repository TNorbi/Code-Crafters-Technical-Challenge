using OneTimePassWebApp.API.Data.Requests.OTP;
using OneTimePassWebApp.API.Data.Requests.Users;
using OneTimePassWebApp.API.Data.Responses.OTP;
using OneTimePassWebApp.API.Data.Responses.Users;

namespace OneTimePassWebApp.API.Services.Users
{
    public interface IUserService
    {
        public Task<AllUsersResponse> getAllUsers();

        public Task<UserResponse> getUserById(int userId);
        public Task<UserResponse> getUserByUsername(string userName);

        public Task<UserResponse> registerNewUser(UserRequest userRequest);

        public Task<UserLoginResponse> loginUser(UserRequest request);

        public Task<OneTimePasswordResponse> generateOTP(OneTimePasswordRequest request);

        public Task<VerifyOTPResponse> verifyOTP(OTPVerifyRequest request);
    }
}
