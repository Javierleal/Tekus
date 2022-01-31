namespace API.DTOs.Users
{
    public class AuthenticateResponse
    {
        public AuthenticateResponse()
        {
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public UserInfoDTO User { get; set; }
        public string Token { get; set; }


    }
}