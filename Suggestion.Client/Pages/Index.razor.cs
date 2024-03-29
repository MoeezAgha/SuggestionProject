﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Suggestion.BL.Model;
using System.Security.Claims;

namespace Suggestion.Client.Pages
{
    public partial class Index : ComponentBase
    {
        public List<Tweet> Tweets { get; set; } = new List<Tweet>();
        [Inject]
        public HttpClient _httpClient { get; set; }


        protected override async void OnInitialized()
        {
            Tweets = await _httpClient.GetFromJsonAsync<List<Tweet>>("api/fetchPost");
            StateHasChanged();
            base.OnInitialized();
        }
        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            return base.OnAfterRenderAsync(firstRender);
        }





        private string authMessage;
        private string surnameMessage;
        private IEnumerable<Claim> claims = Enumerable.Empty<Claim>();
        [Inject]
        AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        private async Task GetClaimsPrincipalData()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
            {
                authMessage = $"{user.Identity.Name} is authenticated.";
                claims = user.Claims;
                surnameMessage =
                    $"Surname: {user.FindFirst(c => c.Type == ClaimTypes.Surname)?.Value}";
            }
            else
            {
                authMessage = "The user is NOT authenticated.";
            }

        }

    }
}
