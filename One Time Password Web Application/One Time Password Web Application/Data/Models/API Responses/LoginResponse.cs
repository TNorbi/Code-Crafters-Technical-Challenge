using One_Time_Password_Web_Application.Data.Models.Users;

namespace One_Time_Password_Web_Application.Data.Models.API_Responses
{
    public class LoginResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public User? User { get; set; }
    }
}
