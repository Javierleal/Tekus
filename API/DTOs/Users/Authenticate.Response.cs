namespace API.DTOs.Users
{
    public class AuthenticateResponse
    {
        public AuthenticateResponse(UserInfoDTO user, string token)
        {
            Id = user.Id;
            Username = user.UserName;
            Token = token;
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }


    }
}