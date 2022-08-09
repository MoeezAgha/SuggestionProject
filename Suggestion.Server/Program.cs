using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Suggestion.BL.Model;
using Suggestion.DAL.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add services to the container.
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//         options.UseSqlite("Filename=data.db"));
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

//builder.Services.AddDbContext<ApplicationDbContext>(x => x.UseSqlite(builder.Configuration.GetConnectionString("ConStr")));

builder.Services.AddDbContext<ApplicationDbContext>(dbcontextOptions => //dbcontextOptions.UseSqlite("Data Source=Bazaristan.sqlite"));
dbcontextOptions.UseSqlite(builder.Configuration.GetConnectionString("ConStr")), ServiceLifetime.Scoped
);

//builder.Services.Configure<IdentityOptions>(options =>
//{
//    // Password settings
//    options.Password.RequireDigit = false;
//    options.Password.RequiredLength = 6;
//    options.Password.RequireNonAlphanumeric = false;
//    options.Password.RequireUppercase = false;
//    options.Password.RequireLowercase = false;

//    // Lockout settings
//    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
//    options.Lockout.MaxFailedAccessAttempts = 10;
//    options.Lockout.AllowedForNewUsers = true;

//    // User settings
//    options.User.RequireUniqueEmail = true;
//});



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region JWT-Token
// NOTE: the following block of code is newly added
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false,
        ValidAudience = builder.Configuration["Audience"],
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JwtIssuer"],
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JwtSecurityKey"])) // NOTE: THIS SHOULD BE A SECRET KEY NOT TO BE SHARED; A GUID IS RECOMMENDED
    };
});



#endregion
// NOTE: end block
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();


app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
