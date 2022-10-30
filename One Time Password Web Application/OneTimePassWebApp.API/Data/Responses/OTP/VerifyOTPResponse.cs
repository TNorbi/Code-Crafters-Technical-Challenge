using OneTimePassWebApp.API.Data.Models;
using System.Text.Json.Serialization;

namespace OneTimePassWebApp.API.Data.Responses.OTP
{
    public class VerifyOTPResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public long Timestamp { get; set; }

        [JsonIgnore]
        public OTPCheckers? Checkers { get; set; }
    }
}
