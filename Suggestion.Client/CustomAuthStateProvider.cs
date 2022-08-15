using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace Suggestion.Client
{

    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _http;





        public CustomAuthStateProvider(ILocalStorageService localStorage, HttpClient http)
        {
            _localStorage = localStorage;
            _http = http;

        }

        public async Task<string> GetTokenAsync()
        {

            return await _localStorage.GetItemAsStringAsync(key: "token");


        }



        //public async Task Login(LoginParameters loginParameters)
        //{
        //    await _authorizeApi.Login(loginParameters);

        //    NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        //}

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            try
            {


                string token = await _localStorage.GetItemAsStringAsync("token");

                //  string token = await _localStorage.GetItemAsStringAsync("token");
                if (!string.IsNullOrEmpty(token))
                {
                    _http.DefaultRequestHeaders.Authorization = null;

                    if (!string.IsNullOrEmpty(token))
                    {
                        identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
                        _http.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
                    }

                    var user = new ClaimsPrincipal(identity);
                    var state = new AuthenticationState(user);

                    NotifyAuthenticationStateChanged(Task.FromResult(state));
                    return state;
                }
                else
                {
                    var user = new ClaimsPrincipal(identity);
                    var state = new AuthenticationState(user);
                    return state;
                }










            }
            catch (Exception e)
            {
                var user = new ClaimsPrincipal(identity);
                var state = new AuthenticationState(user);
                return state;
            }
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
    }

    public class tokem
    {

        public string? token { get; set; }
    }

}
