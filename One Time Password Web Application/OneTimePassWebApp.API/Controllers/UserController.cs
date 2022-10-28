using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneTimePassWebApp.API.Data.Responses.Users;
using OneTimePassWebApp.API.Services.Users;
using OneTimePassWebApp.API.Utils;

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

                return Ok(response);
            }
            catch(Exception e)
            {
                AllUsersResponse exceptionResponse = new AllUsersResponse { Code = 301, Message = APIErrorCodes.GET_ALL_USERS_REQUEST_EXCEPTION_MESSAGE + e.Message };

                return StatusCode(301, exceptionResponse);
            }
        }
    }
}
