using System.Text.Json.Serialization;

namespace OneTimePassWebApp.API.Data.Models
{
    public class OTPCheckers
    {
        public bool IsOTP { get; set; }

        public bool isDateExpired { get; set; }
    }
}
