using System.ComponentModel.DataAnnotations;

namespace vyroute.Models
{
    public class RegisterModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; } = false;
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Gender { get; set; }

        public string PhoneNumber { get; set; }
    }
}
