using Microsoft.AspNetCore.Mvc;
using OneTimePassWebApp.API.Data.Requests.OTP;
using OneTimePassWebApp.API.Data.Requests.Users;
using OneTimePassWebApp.API.Data.Responses.OTP;
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

                if (response.Users != null)
                {
                    return Ok(response);
                }

                return StatusCode(300, response);
            }
            catch (Exception e)
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

        [HttpPost("register-new-user")]
        public async Task<IActionResult> registerNewUser([FromBody] UserRequest userRequest)
        {

            if (String.IsNullOrEmpty(userRequest.UserName))
            {
                UserResponse errorResponse = new UserResponse
                {
                    Code = 300,
                    Message = APIErrorCodes.MISSING_BODY_ERROR_MESSAGE + "UserName"
                };

                return StatusCode(300, errorResponse);
            }

            if (String.IsNullOrEmpty(userRequest.Password))
            {
                UserResponse errorResponse = new UserResponse
                {
                    Code = 301,
                    Message = APIErrorCodes.MISSING_BODY_ERROR_MESSAGE + "Password"
                };

                return StatusCode(301, errorResponse);
            }

            try
            {

                UserResponse response = await _userService.registerNewUser(userRequest);

                if (response.Code == 200)
                {
                    return Ok(response);
                }

                return StatusCode(response.Code, response);

            }
            catch (Exception e)
            {
                UserResponse responseException = new UserResponse
                {
                    Code = 305,
                    Message = APIErrorCodes.REGISTER_NEW_USER_EXCEPTION_MESSAGE + e.Message
                };

                return StatusCode(305, responseException);
            }
        }

        [HttpPost("login-user")]
        public async Task<IActionResult> login([FromBody] UserRequest userRequest)
        {
            if (String.IsNullOrEmpty(userRequest.UserName))
            {
                UserResponse errorResponse = new UserResponse
                {
                    Code = 300,
                    Message = APIErrorCodes.MISSING_BODY_ERROR_MESSAGE + "UserName"
                };

                return StatusCode(300, errorResponse);
            }

            if (String.IsNullOrEmpty(userRequest.Password))
            {
                UserResponse errorResponse = new UserResponse
                {
                    Code = 301,
                    Message = APIErrorCodes.MISSING_BODY_ERROR_MESSAGE + "Password"
                };

                return StatusCode(301, errorResponse);
            }

            try
            {
                UserLoginResponse response = await _userService.loginUser(userRequest);

                if (response.Code == 200)
                {
                    return Ok(response);
                }

                return StatusCode(response.Code, response);
            }
            catch (Exception e)
            {
                UserResponse exceptionResponse = new UserResponse
                {
                    Code = 303,
                    Message = APIErrorCodes.LOGIN_USER_EXCEPTION_MESSAGE + e.Message
                };

                return StatusCode(303, exceptionResponse);
            }
        }

        [HttpPost("generate-one-time-password")]
        public async Task<IActionResult> generateOneTimePassword([FromBody] OneTimePasswordRequest request)
        {
            if (String.IsNullOrEmpty(request.UserID.ToString()))
            {
                OneTimePasswordResponse errorResponse = new OneTimePasswordResponse
                {
                    Code = 300,
                    Message = APIErrorCodes.MISSING_BODY_ERROR_MESSAGE + "UserID",
                    Timestamp = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds
                };

                return StatusCode(300, errorResponse);
            }

            if(request.UserID <= 0)
            {
                OneTimePasswordResponse errorResponse = new OneTimePasswordResponse
                {
                    Code = 301,
                    Message = APIErrorCodes.GENERATE_OPT_USERID_NEGATIVE_ERROR_MESSAGE,
                    Timestamp = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds
                };

                return StatusCode(301, errorResponse);
            }

            if (String.IsNullOrEmpty(request.DateTime.ToString()))
            {
                OneTimePasswordResponse errorResponse = new OneTimePasswordResponse
                {
                    Code = 302,
                    Message = APIErrorCodes.MISSING_BODY_ERROR_MESSAGE + "DateTime",
                    Timestamp = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds
                };

                return StatusCode(302, errorResponse);
            }

            if(!DateTime.TryParse(request.DateTime.ToString(), out DateTime date))
            {
                OneTimePasswordResponse errorResponse = new OneTimePasswordResponse
                {
                    Code = 303,
                    Message = APIErrorCodes.GENERATE_OTP_DATETIME_NOT_VALID_ERROR_MESSAGE,
                    Timestamp = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds
                };

                return StatusCode(303, errorResponse);
            }

            try
            {
                OneTimePasswordResponse response = await _userService.generateOTP(request);

                if(response.Code == 200)
                {
                    return Ok(response);
                }

                //return StatusCode(response.Code, response); //response body doesn't appear for some reason, only the response code
                return Ok(response);

            }
            catch(Exception e)
            {
                OneTimePasswordResponse errorResponse = new OneTimePasswordResponse
                {
                    Code = 400,
                    Message = APIErrorCodes.GENERATE_OTP_EXCEPTION_MESSAGE + e.Message,
                    Timestamp = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds
                };

                return StatusCode(400, errorResponse);
            }
        }

        [HttpPost("verify-one-time-password")]
        public async Task<IActionResult> verifyOneTimePassword([FromBody] OTPVerifyRequest request)
        {
            if (String.IsNullOrEmpty(request.UserID.ToString()))
            {
                VerifyOTPResponse errorResponse = new VerifyOTPResponse
                {
                    Code = 300,
                    Message = APIErrorCodes.MISSING_BODY_ERROR_MESSAGE + "UserID",
                    Timestamp = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds
                };

                return StatusCode(300, errorResponse);
            }

            if (request.UserID <= 0)
            {
                VerifyOTPResponse errorResponse = new VerifyOTPResponse
                {
                    Code = 301,
                    Message = APIErrorCodes.GENERATE_OPT_USERID_NEGATIVE_ERROR_MESSAGE,
                    Timestamp = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds
                };

                return StatusCode(301, errorResponse);
            }

            if (String.IsNullOrEmpty(request.DateTime.ToString()))
            {
                VerifyOTPResponse errorResponse = new VerifyOTPResponse
                {
                    Code = 302,
                    Message = APIErrorCodes.MISSING_BODY_ERROR_MESSAGE + "DateTime",
                    Timestamp = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds
                };

                return StatusCode(302, errorResponse);
            }

            if (!DateTime.TryParse(request.DateTime.ToString(), out DateTime date))
            {
                VerifyOTPResponse errorResponse = new VerifyOTPResponse
                {
                    Code = 303,
                    Message = APIErrorCodes.GENERATE_OTP_DATETIME_NOT_VALID_ERROR_MESSAGE,
                    Timestamp = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds
                };

                return StatusCode(303, errorResponse);
            }

            if (String.IsNullOrEmpty(request.EnteredOTP))
            {
                VerifyOTPResponse errorResponse = new VerifyOTPResponse
                {
                    Code = 304,
                    Message = APIErrorCodes.MISSING_BODY_ERROR_MESSAGE + "Entered OTP",
                    Timestamp = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds
                };

                return StatusCode(304, errorResponse);
            }

            if (String.IsNullOrEmpty(request.ExpireDate.ToString()))
            {
                VerifyOTPResponse errorResponse = new VerifyOTPResponse
                {
                    Code = 305,
                    Message = APIErrorCodes.MISSING_BODY_ERROR_MESSAGE + "ExpireDate",
                    Timestamp = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds
                };

                return StatusCode(305, errorResponse);
            }

            if (!DateTime.TryParse(DateTimeOffset.FromUnixTimeSeconds((long)request.ExpireDate).ToString(), out DateTime date2))
            {
                VerifyOTPResponse errorResponse = new VerifyOTPResponse
                {
                    Code = 306,
                    Message = APIErrorCodes.GENERATE_OTP_DATETIME_NOT_VALID_ERROR_MESSAGE,
                    Timestamp = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds
                };

                return StatusCode(306, errorResponse);
            }

            if (String.IsNullOrEmpty(request.OriginalOTP))
            {
                VerifyOTPResponse errorResponse = new VerifyOTPResponse
                {
                    Code = 307,
                    Message = APIErrorCodes.MISSING_BODY_ERROR_MESSAGE + "Original OTP",
                    Timestamp = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds
                };

                return StatusCode(307, errorResponse);
            }

            try
            {

                VerifyOTPResponse response = await _userService.verifyOTP(request);

                return Ok(response);

            }catch(Exception e)
            {
                VerifyOTPResponse errorResponse = new VerifyOTPResponse
                {
                    Code = 500,
                    Message = APIErrorCodes.VERIFY_OTP_EXCEPTION_MESSAGE + e.Message,
                    Timestamp = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds
                };

                return StatusCode(500, errorResponse);
            }
        }
    }
}
