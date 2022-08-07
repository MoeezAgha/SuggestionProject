namespace Suggestion.Shared.Model.ViewModel
{
    public class LoginResult
    {
        public string message { get; set; }
        public string email { get; set; }
        public string jwtBearer { get; set; }
        public bool success { get; set; }
    }
}