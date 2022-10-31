namespace One_Time_Password_Web_Application.Data.Models.API_Responses
{
    public class RegisterResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public Users.UserForRegisterDTO? User { get; set; }
    }
}
