using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 10;
    options.Lockout.AllowedForNewUsers = true;

    // User settings
    options.User.RequireUniqueEmail = true;
});



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
