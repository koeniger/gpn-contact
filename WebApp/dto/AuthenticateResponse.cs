using Models.secr;
using System;

namespace WebApp.dto
{
    public class AuthenticateResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }

        public string ContactInfo { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse()
        { 
        
        }

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