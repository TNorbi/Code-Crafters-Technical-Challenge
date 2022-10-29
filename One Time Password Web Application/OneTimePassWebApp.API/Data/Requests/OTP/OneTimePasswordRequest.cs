using System.ComponentModel.DataAnnotations;

namespace OneTimePassWebApp.API.Data.Requests.OTP
{
    public class OneTimePasswordRequest
    {
        public int UserID { get; set; }
        public DateTime DateTime { get; set; }
    }
}
