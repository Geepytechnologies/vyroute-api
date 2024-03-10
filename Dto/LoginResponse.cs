namespace vyroute.Dto
{
    public class LoginResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public UserLoginResponseDetails Result { get; set; }
    }

}
