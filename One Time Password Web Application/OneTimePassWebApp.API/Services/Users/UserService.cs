﻿using OneTimePassWebApp.API.Data.Models;
using OneTimePassWebApp.API.Data.Responses.Users;
using OneTimePassWebApp.API.Repositories.Users;
using OneTimePassWebApp.API.Utils;

namespace OneTimePassWebApp.API.Services.Users
{
    public class UserService: IUserService
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

                if (response.Users != null) {

                    response.Code = 200;
                    response.Message = APISuccessCodes.GET_ALL_USERS_SUCCESS_MESSAGE;
                }
                else
                {
                    response.Code = 300;
                    response.Message = APIErrorCodes.GET_ALL_USERS_NULL_MESSAGE;
                }

                return response;
                

            }catch(Exception e)
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
                else {
                    response.Code = 300;
                    response.Message = APIErrorCodes.GET_USER_BY_USERID_NULL_MESSAGE;
                }

                return response;

            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<AllUsersResponse> getUserByUsername(string userName)
        {
            AllUsersResponse response = new AllUsersResponse();

            try
            {
                response.Users = await _userRepository.getUserByUsername(userName);

                if (response.Users != null && response.Users.Count() > 0)
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
    }
}
