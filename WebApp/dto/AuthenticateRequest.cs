using System.ComponentModel.DataAnnotations;

namespace WebApp.dto
{
    public class AuthenticateRequest
    {
        [Required]
        public string UserEmail { get; set; }

        [Required]
        public string Password { get; set; }
    }
}