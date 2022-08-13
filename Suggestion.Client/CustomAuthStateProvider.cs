using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Suggestion.Shared.Model.ViewModel;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace Suggestion.Client
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        //  private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _httpClient;

        private UserInfo _userInfoCache;
        private readonly IAuthorizeApi _authorizeApi;
        private readonly ILocalStorageService _localStorage;



        public CustomAuthStateProvider(IAuthorizeApi authorizeApi, ILocalStorageService localStorage)
        {
            this._authorizeApi = authorizeApi;
            this._localStorage = localStorage;

        }



        public async Task Login(LoginParameters loginParameters)
        {
            await _authorizeApi.Login(loginParameters);

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task Register(RegisterParameters registerParameters)
        {
            await _authorizeApi.Register(registerParameters);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task Logout()
        {
            await _authorizeApi.Logout();
            _userInfoCache = null;
            await _localStorage.RemoveItemAsync("token");
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        private async Task<UserInfo> GetUserInfo()
        {
            if (_userInfoCache != null && _userInfoCache.IsAuthenticated) return _userInfoCache;
            _userInfoCache = await _authorizeApi.GetUserInfo();
            return _userInfoCache;
        }

        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }




        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {


            var identity = new ClaimsIdentity();

            try
            {
                try
                {
                    var token = await _localStorage.GetItemAsStringAsync("token");

                    var jwthandler = new JwtSecurityTokenHandler();
                    var jwttoken = jwthandler.ReadToken(token as string);
                    var expDate = jwttoken.ValidTo;
                    if (expDate < DateTime.UtcNow.AddMinutes(1))
                    {

                    }



                    identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
                    _httpClient.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));

                    var user = new ClaimsPrincipal(identity);
                    var state = new AuthenticationState(user);
                    NotifyAuthenticationStateChanged(Task.FromResult(state));


                }
                catch (Exception e)
                {


                }
                //    var result = await protectedSessionStorage.GetAsync<UserInfo>("UserInfo");
                // bug on http.context using SessionStroageService
                //  protectedSessionStorage


            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Request failed:" + ex.ToString());
            }



            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        private async Task<ClaimsIdentity> GetClaimsWithToken(string jwt)
        {

            var identity = new ClaimsIdentity();
            //  _httpClient.DefaultRequestHeaders.Authorization = null;

            identity = new ClaimsIdentity(ParseClaimsFromJwt(jwt), "jwt");
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", jwt.Replace("\"", ""));


            return identity;

        }
    }
}
