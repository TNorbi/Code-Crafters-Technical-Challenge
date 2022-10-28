namespace OneTimePassWebApp.API.Data.Responses.Users
{
    public class UserResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public Data.Models.Users? User { get; set; }
    }
}
