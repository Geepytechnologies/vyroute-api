namespace vyroute.Dto
{
    public class UserLoginResponseDetails
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Phone { get; set; }

        public string AccessToken { get; set; }

        public bool IsAdmin { get; set; }
    }
}
