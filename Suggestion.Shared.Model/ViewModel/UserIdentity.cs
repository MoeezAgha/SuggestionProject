namespace Suggestion.Shared.Model.ViewModel
{
    public class UserIdentity : IDisposable
    {
        public event EventHandler TokenRefreshRequired;
        private double SessionRefreshHours { get; set; }
        private DateTime TokenExpirationTime { get; set; }
        public string BearerToken { get; private set; }
        public string Id { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public bool IsAuthenticated { get; private set; }
        public List<string> Roles { get; private set; }
        public void Dispose()
        {
            BearerToken = String.Empty;
            Id = String.Empty;
            UserName = String.Empty;
            Email = String.Empty;
            Roles = new List<string>();

            IsAuthenticated = false;
        }

    }

}