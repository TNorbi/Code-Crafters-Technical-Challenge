using System.ComponentModel.DataAnnotations;

namespace OneTimePassWebApp.API.Data.Requests.Users
{
    public class OneTimePasswordRequest
    {
        public int UserID { get; set; }
        public DateTime DateTime { get; set; }
    }
}
