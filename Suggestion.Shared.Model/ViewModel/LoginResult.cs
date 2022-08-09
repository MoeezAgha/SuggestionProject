namespace Suggestion.Shared.Model.ViewModel
{
    public class LoginResult
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string JwtBearer { get; set; }
        public bool Success { get; set; }

        public string Role { get; set; }
    }
}