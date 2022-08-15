using Microsoft.AspNetCore.Components;
using Suggestion.Shared.Model.ViewModel;

namespace BlazorApp1.Pages
{
    public partial class Login : ComponentBase
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

        //protected async Task Login()
        //{

        //    _ = _IAuthorizeApi.Login(LoginParameters);




        //    navigationManager.NavigateTo("/");

        //}


    }

    //public class AuthorizeApi : IAuthorizeApi
    //{
    //    private readonly HttpClient _httpClient;
    //    private readonly ILocalStorageService _localStorage;
    //    private readonly AuthenticationStateProvider _authStateProvider;

    //    public AuthorizeApi(HttpClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authStateProvider)
    //    {
    //        _httpClient = httpClient;
    //        this._localStorage = localStorage;
    //        this._authStateProvider = authStateProvider;
    //    }

    //    public async Task Login(LoginParameters loginParameters)
    //    {
    //        //var stringContent = new StringContent(JsonSerializer.Serialize(loginParameters), Encoding.UTF8, "application/json");
    //        var result = await _httpClient.PostAsJsonAsync("api/Login", loginParameters);
    //        result.EnsureSuccessStatusCode();
    //        if (result.IsSuccessStatusCode)
    //        {
    //            var token = await result.Content.ReadAsStringAsync();
    //            await _localStorage.SetItemAsync("token", token);
    //            await _authStateProvider.GetAuthenticationStateAsync();

    //        }
    //        if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());



    //    }

    //    public async Task Logout()
    //    {
    //        var result = await _httpClient.PostAsync("api/Authorize/Logout", null);
    //        result.EnsureSuccessStatusCode();
    //    }

    //    public async Task Register(RegisterParameters registerParameters)
    //    {
    //        //var stringContent = new StringContent(JsonSerializer.Serialize(registerParameters), Encoding.UTF8, "application/json");
    //        var result = await _httpClient.PostAsJsonAsync("api/Authorize/Register", registerParameters);
    //        if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
    //        result.EnsureSuccessStatusCode();
    //    }

    //    public async Task<UserInfo> GetUserInfo()
    //    {
    //        var result = await _httpClient.GetFromJsonAsync<UserInfo>("api/Authorize/UserInfo");
    //        return result;
    //    }
    //}
    public interface IAuthorizeApi
    {
        Task Login(LoginParameters loginParameters);
        Task Register(RegisterParameters registerParameters);
        Task Logout();
        Task<UserInfo> GetUserInfo();
    }
}
