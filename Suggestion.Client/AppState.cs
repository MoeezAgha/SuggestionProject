//using Blazored.LocalStorage;
//using Suggestion.Server;
//using Suggestion.Shared.Model.ViewModel;
//using System.Net.Http.Headers;
//using System.Security.Claims;
//using System.Text;
//using System.Text.Json;

//namespace Suggestion.Client
//{
//    public class AppState
//    {
//        private readonly HttpClient _httpClient;
//        private readonly ILocalStorageService _localStorage;

//        public bool IsLoggedIn { get; private set; }

//        public AppState(HttpClient httpClient,
//                        ILocalStorageService localStorage)
//        {
//            _httpClient = httpClient;
//            _localStorage = localStorage;
//            SetAuthorizationHeader();
//        }

//        public async Task Login(LoginDetails loginDetails)
//        {
//            var response = await _httpClient.PostAsync(Urls.Login, new StringContent(JsonSerializer.Serialize(loginDetails), Encoding.UTF8, mediaType: "application/json"));

//            if (response.IsSuccessStatusCode)
//            {

//                await SaveToken(response);
//                await SetAuthorizationHeader();

//                IsLoggedIn = true;
//            }
//        }

//        public async Task Logout()
//        {
//            await _localStorage.RemoveItemAsync("authToken");
//            IsLoggedIn = false;
//        }

//        private async Task SaveToken(HttpResponseMessage response)
//        {
//            var responseContent = await response.Content.ReadAsStringAsync();
//            var jwt = JsonSerializer.Deserialize<JwToken>(responseContent);







//            await _localStorage.SetItemAsync("authToken", jwt.token);
//        }
//        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
//        {
//            var payload = jwt.Split('.')[1];
//            var jsonBytes = ParseBase64WithoutPadding(payload);
//            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
//            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
//        }
//        private static byte[] ParseBase64WithoutPadding(string base64)
//        {
//            switch (base64.Length % 4)
//            {
//                case 2: base64 += "=="; break;
//                case 3: base64 += "="; break;
//            }
//            return Convert.FromBase64String(base64);
//        }

//        private async Task SetAuthorizationHeader()
//        {
//            if (!_httpClient.DefaultRequestHeaders.Contains("Authorization"))
//            {
//                var token = await _localStorage.GetItemAsync<string>("authToken");


//                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
//            }
//        }

//        private async Task<ClaimsIdentity> GetClaimsWithToken(string jwt)
//        {

//            var identity = new ClaimsIdentity();
//            _httpClient.DefaultRequestHeaders.Authorization = null;

//            identity = new ClaimsIdentity(ParseClaimsFromJwt(jwt), "jwt");
//            _httpClient.DefaultRequestHeaders.Authorization =
//                new AuthenticationHeaderValue("Bearer", jwt.Replace("\"", ""));


//            return identity;


//        }


//    }
//}
