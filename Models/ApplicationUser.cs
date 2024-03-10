using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace vyroute.Models
{
    public class ApplicationUser: IdentityUser<Guid>
    {
        public bool IsAdmin { get; set; }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Gender { get; set; }

        [StringLength(13, ErrorMessage = "Phone must be 13 characters long.")]
        public override string PhoneNumber { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiry {  get; set; }
    }
}
