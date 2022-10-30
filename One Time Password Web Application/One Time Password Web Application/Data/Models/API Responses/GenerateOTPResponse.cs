namespace One_Time_Password_Web_Application.Data.Models.API_Responses
{
    public class GenerateOTPResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public string? OTP { get; set; }

        public long Timestamp { get; set; }
    }
}
