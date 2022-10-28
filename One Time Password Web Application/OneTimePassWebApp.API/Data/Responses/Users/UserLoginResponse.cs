namespace OneTimePassWebApp.API.Data.Responses.Users
{
    public class UserLoginResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public Data.Models.DTOs.Users.UserLoginDTO? User { get; set; }
    }
}
