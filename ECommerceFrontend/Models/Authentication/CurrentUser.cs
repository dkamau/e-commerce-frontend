namespace ECommerceFrontend.Models.Authentication
{
    public class CurrentUser
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }
        public bool EmailIsConfirmed { get; set; } = false;
    }
}
