using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneTimePassWebApp.API.Data.Requests.Users;
using OneTimePassWebApp.API.Data.Responses.Users;
using OneTimePassWebApp.API.Services.Users;
using OneTimePassWebApp.API.Utils;
using System.ComponentModel.DataAnnotations;

namespace OneTimePassWebApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService { get; set; }

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("get-all-users")]
        public async Task<IActionResult> getAllUsers()
        {

            try
            {
                AllUsersResponse response = await _userService.getAllUsers();

                if(response.Users != null)
                {
                    return Ok(response);
                }

                return StatusCode(300, response);
            }
            catch(Exception e)
            {
                AllUsersResponse exceptionResponse = new AllUsersResponse { Code = 301, Message = APIErrorCodes.GET_ALL_USERS_REQUEST_EXCEPTION_MESSAGE + e.Message };

                return StatusCode(301, exceptionResponse);
            }
        }

        [HttpGet("get-user-by-username")]
        public async Task<IActionResult> getUserByUsername([Required][FromHeader] string userName)
        {
            try
            {
                UserResponse response = await _userService.getUserByUsername(userName);

                if(response.User != null)
                {
                    return Ok(response);
                }
                
                return StatusCode(300, response);

            }catch(Exception e)
            {
                UserResponse exceptionResponse = new UserResponse
                { Code = 301, Message = APIErrorCodes.GET_USER_BY_USERNAME_REQUEST_EXCEPTION_MESSAGE + e.Message };

                return StatusCode(301, exceptionResponse);
            }
        }

        [HttpGet("get-user-by-userid")]
        public async Task<IActionResult> getUserByUserID([Required][FromHeader] int userID)
        {
            try
            {
                UserResponse response = await _userService.getUserById((int)userID);

                if (response.User != null)
                {
                    return Ok(response);
                }

                return StatusCode(300, response);

            }
            catch (Exception e)
            {
                UserResponse exceptionResponse = new UserResponse
                { Code = 301, Message = APIErrorCodes.GET_USER_BY_USERNAME_REQUEST_EXCEPTION_MESSAGE + e.Message };

                return StatusCode(301, exceptionResponse);
            }
        }

        /*[HttpPost("register-new-user")]
        public async Task<IActionResult> registerNewUser([FromBody] UserRequest userRequest){

            if (String.IsNullOrEmpty(userRequest.UserName))
            {
                UserResponse userResponse = new UserResponse
                {
                    Code = 300,
                    Message = APIErrorCodes.
                }
                return StatusCode(300, errorResponse);
            }

            return Ok();
        }*/
    }
}
