using Microsoft.AspNetCore.Components;
using Suggestion.Shared.Model.ViewModel;

namespace Suggestion.Client.Pages
{
    public class LoginModel : ComponentBase
    {
        [Inject] private AppState _appState { get; set; }
        // [Inject] private Microsoft.AspNetCore.Components.IUriHelper _uriHelper { get; set; }

        protected LoginDetails LoginDetails { get; set; } = new LoginDetails();
        protected bool ShowLoginFailed { get; set; }

        protected async Task Login()
        {
            await _appState.Login(LoginDetails);

            if (_appState.IsLoggedIn)
            {
                //    _uriHelper.NavigateTo("/");
            }
            else
            {
                ShowLoginFailed = true;
            }
        }
    }
}
