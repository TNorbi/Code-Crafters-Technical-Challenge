using OneTimePassWebApp.API.Data.Models;
using System.Text.Json.Serialization;

namespace OneTimePassWebApp.API.Data.Responses.OTP
{
    public class VerifyOTPResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public double Timestamp { get; set; }

        [JsonIgnore]
        public OTPCheckers? Checkers { get; set; }
    }
}
