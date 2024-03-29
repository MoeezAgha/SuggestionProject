
using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Suggestion.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
//builder.Services.AddScoped<AppState>();


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();



//builder.Services.AddLocalStorage();
builder.Services.AddHttpContextAccessor();


#region Working

//builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
#endregion

//builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<CustomAuthStateProvider>());
//builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

builder.Services.AddTransient<IAuthorizeApi, AuthorizeApi>();
//builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

builder.Services.AddAuthorizationCore();

//builder.Services.AddBlazoredSessionStorage();
builder.Services.AddBlazoredLocalStorage();

//builder.Services.AddBlazoredLocalStorage(config =>
//    config.JsonSerializerOptions.WriteIndented = true
//);

//builder.Services.AddBlazoredSessionStorage(config =>
//    config.JsonSerializerOptions.WriteIndented = true
//);

builder.Services.AddBlazoredSessionStorage();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7168/") });

builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider =>
      provider.GetRequiredService<CustomAuthStateProvider>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
