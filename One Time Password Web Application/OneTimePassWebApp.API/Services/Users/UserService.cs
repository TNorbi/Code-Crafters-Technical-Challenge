using Microsoft.AspNetCore.Identity;
using OneTimePassWebApp.API.Data.Requests.OTP;
using OneTimePassWebApp.API.Data.Requests.Users;
using OneTimePassWebApp.API.Data.Responses.OTP;
using OneTimePassWebApp.API.Data.Responses.Users;
using OneTimePassWebApp.API.Repositories.Users;
using OneTimePassWebApp.API.Utils;

namespace OneTimePassWebApp.API.Services.Users
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository { get; }

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<AllUsersResponse> getAllUsers()
        {
            AllUsersResponse response = new AllUsersResponse();

            try
            {
                response.Users = await _userRepository.getAllUsers();

                if (response.Users != null)
                {

                    response.Code = 200;
                    response.Message = APISuccessCodes.GET_ALL_USERS_SUCCESS_MESSAGE;
                }
                else
                {
                    response.Code = 300;
                    response.Message = APIErrorCodes.GET_ALL_USERS_NULL_MESSAGE;
                }

                return response;


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<UserResponse> getUserById(int userId)
        {
            UserResponse response = new UserResponse();

            try
            {
                response.User = await _userRepository.getUserByUserId(userId);

                if (response.User != null)
                {
                    response.Code = 200;
                    response.Message = APISuccessCodes.GET_USER_BY_USERID_SUCCES_MESSAGE;
                }
                else
                {
                    response.Code = 300;
                    response.Message = APIErrorCodes.GET_USER_BY_USERID_NULL_MESSAGE;
                }

                return response;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<UserResponse> getUserByUsername(string userName)
        {
            UserResponse response = new UserResponse();

            try
            {
                response.User = await _userRepository.getUserByUsername(userName);

                if (response.User != null)
                {
                    response.Code = 200;
                    response.Message = APISuccessCodes.GET_USER_BY_USERNAME_SUCCES_MESSAGE;
                }
                else
                {
                    response.Code = 300;
                    response.Message = APIErrorCodes.GET_USER_BY_USERNAME_NULL_MESSAGE;
                }

                return response;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<UserResponse> registerNewUser(UserRequest userRequest)
        {
            //Hashes the user's password
            PasswordHasher<string> passwordHasher = new PasswordHasher<string>();


            Data.Models.Users NewUser = new Data.Models.Users
            {
                UserName = userRequest.UserName,
                Password = passwordHasher.HashPassword(userRequest.UserName, userRequest.Password)
            };

            UserResponse response = new UserResponse();

            try
            {
                response.User = await _userRepository.registerNewUser(NewUser);

                if (response.User != null)
                {
                    if (response.User.UserId == -1)
                    {
                        response.Code = 302;
                        response.Message = APIErrorCodes.REGISTER_NEW_USER_PASSWORD_EXISTS_ERROR_MESSAGE;
                    }
                    else if (response.User.UserId == -2)
                    {
                        response.Code = 303;
                        response.Message = APIErrorCodes.REGISTER_NEW_USER_USERNAME_EXISTS_ERROR_MESSAGE;
                    }
                    else
                    {
                        response.Code = 200;
                        response.Message = APISuccessCodes.REGISTER_NEW_USER_SUCCESS_MESSAGE;
                    }
                }
                else
                {
                    response.Code = 304;
                    response.Message = APIErrorCodes.REGISTER_NEW_USER_NULL_ERROR_MESSAGE;
                }

                return response;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<UserLoginResponse> loginUser(UserRequest request)
        {
            Data.Models.Users User = new Data.Models.Users
            {
                UserName = request.UserName,
                Password = request.Password
            };

            UserLoginResponse response = new UserLoginResponse();

            try
            {
                response.User = await _userRepository.loginUser(User);

                if (response.User != null)
                {
                    response.Code = 200;
                    response.Message = APISuccessCodes.LOGIN_USER_SUCCESS_MESSAGE;
                }
                else
                {
                    response.Code = 302;
                    response.Message = APIErrorCodes.LOGIN_USER_NULL_MESSAGE;
                }

                return response;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<OneTimePasswordResponse> generateOTP(OneTimePasswordRequest request)
        {
            OneTimePasswordResponse response = new OneTimePasswordResponse();

            try
            {
                response.OTP = await _userRepository.generateOTP(request.UserID);

                if(!String.IsNullOrEmpty(response.OTP))
                {
                    response.Code = 200;
                    response.Message = APISuccessCodes.GENERATE_OTP_SUCCES_MESSAGE;
                    DateTime expireDate = request.DateTime.AddSeconds(30);
                    response.Timestamp = expireDate.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                }
                else
                {
                    response.Code = 304;
                    response.Message = APIErrorCodes.GET_USER_BY_USERID_NULL_MESSAGE;
                    response.Timestamp = request.DateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                }

                return response;

            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<VerifyOTPResponse> verifyOTP(OTPVerifyRequest request)
        {
            VerifyOTPResponse response = new VerifyOTPResponse();

            try
            {
                response.Checkers = await _userRepository.verifyOTP(request);

                if(response.Checkers == null)
                {
                    response.Code = 309;
                    response.Message = APIErrorCodes.GET_USER_BY_USERID_NULL_MESSAGE;
                    return response;
                }


                if (response.Checkers.IsOTP && !response.Checkers.isDateExpired)
                {
                    response.Code = 200;
                    response.Message = APISuccessCodes.VERIFY_OTP_SUCCES_MESSAGE;
                }
                else
                {
                    if (response.Checkers.isDateExpired)
                    {
                        response.Code = 309;
                        response.Message = APIErrorCodes.VERIFY_OTP_DATETIME_EXPIRED_ERROR_MESSAGE;
                    }
                    else
                    {
                        response.Code = 310;
                        response.Message = APIErrorCodes.VERIFY_OTP_WRONG_OTP_ERROR_MESSAGE;
                    }
                }

                return response;

            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
