namespace OneTimePassWebApp.API.Utils
{
    public class APIErrorCodes
    {
        public static readonly string GET_ALL_USERS_NULL_MESSAGE = "Get all users request was unsuccessfull. Please try again!";
        public static readonly string GET_ALL_USERS_REQUEST_EXCEPTION_MESSAGE = "GET_ALL_USERS request was unsuccessful, with the following error message: ";
        public static readonly string MISSING_HEADER_ERROR_MESSAGE = "The following header value is missing: ";
        public static readonly string GET_USER_BY_USERNAME_REQUEST_EXCEPTION_MESSAGE = "GET_USER_BY_USERNAME request was unsuccessful, with the following error message: ";
        public static readonly string GET_USER_BY_USERID_NULL_MESSAGE = "User with given userID doesn't exist in database";
        public static readonly string GET_USER_BY_USERNAME_NULL_MESSAGE = "User with given Username doesn't exist in database";
        public static readonly string MISSING_BODY_ERROR_MESSAGE = "The following body value is missing: ";
        public static readonly string REGISTER_NEW_USER_EXCEPTION_MESSAGE = "REGISTER_NEW_USER request was unsuccessful, with the following error message: ";
        public static readonly string REGISTER_NEW_USER_USERNAME_EXISTS_ERROR_MESSAGE = "User with given username already exists in database";
        public static readonly string REGISTER_NEW_USER_PASSWORD_EXISTS_ERROR_MESSAGE = "User with given password already exists in database";
        public static readonly string REGISTER_NEW_USER_NULL_ERROR_MESSAGE = "Something went wrong during registration. Please try again!";
        public static readonly string LOGIN_USER_EXCEPTION_MESSAGE = "LOGIN_USER request was unsuccessful, with the following error message: ";
        public static readonly string LOGIN_USER_NULL_MESSAGE = "User doesn't exist with given username or password!";
        public static readonly string GENERATE_OPT_USERID_NEGATIVE_ERROR_MESSAGE = "UserID is a negative value or equals 0. It should be a value greater than 0";
        public static readonly string GENERATE_OTP_DATETIME_NOT_VALID_ERROR_MESSAGE = "DateTime is not valid! Please give a valid DateTime value!";
        public static readonly string GENERATE_OTP_EXCEPTION_MESSAGE = "GENERATE_OTP request was unsuccessful, with the following error message: ";
        public static readonly string VERIFY_OTP_WRONG_OTP_ERROR_MESSAGE = "Wrong OTP was given";
        public static readonly string VERIFY_OTP_DATETIME_EXPIRED_ERROR_MESSAGE = "OTP expired after 30s";
        public static readonly string VERIFY_OTP_EXCEPTION_MESSAGE = "VERIFY_OTP request was unsuccessful, with the following error message: ";
    }
}
