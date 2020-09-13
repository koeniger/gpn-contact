using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class AuthenticateRequest
    {
        [Required]
        public string UserEmail { get; set; }

        [Required]
        public string Password { get; set; }
    }
}