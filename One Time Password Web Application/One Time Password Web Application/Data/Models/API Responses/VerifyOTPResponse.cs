namespace One_Time_Password_Web_Application.Data.Models.API_Responses
{
    public class VerifyOTPResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public long Timestamp { get; set; }
    }
}
