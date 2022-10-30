namespace OneTimePassWebApp.API.Data.Requests.OTP
{
    public class OTPVerifyRequest
    {
        public int UserID { get; set; }
        public DateTime DateTime { get; set; }
        public string EnteredOTP { get; set; }
        public long ExpireDate { get; set; }
        public string OriginalOTP { get; set; }
    }
}
