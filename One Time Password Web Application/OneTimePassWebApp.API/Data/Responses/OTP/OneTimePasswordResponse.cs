namespace OneTimePassWebApp.API.Data.Responses.OTP
{
    public class OneTimePasswordResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public string? OTP { get; set; }

        public double Timestamp { get; set; }
    }
}
