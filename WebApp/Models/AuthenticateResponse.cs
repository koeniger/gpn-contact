using Models.secr;

namespace WebApp.Models
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }

        public string ContactInfo { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(user user, string token)
        {
            Id = user.user_id;
            Email = user.email;
            Name = user.user_name;
            Position = user.position;
            ContactInfo = user.contact_info;
            Token = token;
        }
    }
}