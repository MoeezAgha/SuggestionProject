﻿using Blazored.LocalStorage;
using Suggestion.Server;
using Suggestion.Shared.Model.ViewModel;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Suggestion.Client
{
    public class AppState
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public bool IsLoggedIn { get; private set; }

        public AppState(HttpClient httpClient,
                        ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public async Task Login(LoginDetails loginDetails)
        {
            var response = await _httpClient.PostAsync(Urls.Login, new StringContent(JsonSerializer.Serialize(loginDetails), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                await SaveToken(response);
                await SetAuthorizationHeader();

                IsLoggedIn = true;
            }
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            IsLoggedIn = false;
        }

        private async Task SaveToken(HttpResponseMessage response)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var jwt = JsonSerializer.Deserialize<JwToken>(responseContent);

            await _localStorage.SetItemAsync("authToken", jwt.Token);
        }

        private async Task SetAuthorizationHeader()
        {
            if (!_httpClient.DefaultRequestHeaders.Contains("Authorization"))
            {
                var token = await _localStorage.GetItemAsync<string>("authToken");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}
