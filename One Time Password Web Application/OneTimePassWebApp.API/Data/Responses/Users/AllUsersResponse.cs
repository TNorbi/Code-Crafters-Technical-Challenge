namespace OneTimePassWebApp.API.Data.Responses.Users
{
    public class AllUsersResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public IEnumerable<Data.Models.Users> Users { get; set; }
    }
}
