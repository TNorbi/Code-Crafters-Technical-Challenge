namespace One_Time_Password_Web_Application.Data.Models.Login
{
    public class VerifyOTPModel
    {
        public int UserID { get; set; }
        public DateTime DateTime { get; set; }
        public string EnteredOTP { get; set; }
        public long ExpireDate { get; set; }
        public string OriginalOTP { get; set; }
    }
}
