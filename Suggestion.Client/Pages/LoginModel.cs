
using Microsoft.AspNetCore.Components;
using Suggestion.Shared.Model.ViewModel;

namespace Suggestion.Client.Pages
{
    public class LoginModel : ComponentBase
    {
        //  [Inject] private AppState _appState { get; set; }
        [Inject]
        NavigationManager navigationManager { get; set; }
        //[Inject]
        //CustomAuthStateProvider AuthStateProvider { get; set; }
        [Inject]
        NavigationManager _uriHelper { get; set; }

        [Inject]
        IAuthorizeApi _IAuthorizeApi { get; set; }

        protected LoginParameters LoginParameters { get; set; } = new LoginParameters();
        protected bool ShowLoginFailed { get; set; }

        protected async Task Login()
        {

            _ = _IAuthorizeApi.Login(LoginParameters);




            navigationManager.NavigateTo("/");

        }


    }
}
