using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using MultiAuth.Auth;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = "AzureUser";
        options.DefaultChallengeScheme = "AzureUser";

    })
    .AddJwtBearer("AzureUser", options =>
{
    options.Authority = "https://login.microsoftonline.com";
    options.Audience = "your_audience";
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = "https://login.microsoftonline.com",
        ValidateAudience = true,
        ValidAudience = "your_audience",
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey("uaouorzszljaailjftpgamsbiqwftkteccbzanugsramhubiysnmcceullobxatdwrrdcxmxmtwkpbqgdbqiwhubdyrcjspsfgqiaprmrlybfvvhfwxogfptawxvlgnh"u8.ToArray())
    };
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine("Authentication failed: " + context.Exception.Message);
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            var issuer = context.SecurityToken.Issuer;
            Console.WriteLine($"Token validated successfully. Issuer: {issuer}");
            return Task.CompletedTask;
        }
    };
}).AddJwtBearer("SignInUser", options =>
{
    options.Authority = "https://signin.company.com";
    options.Audience = "your_audience";
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = "https://signin.company.com",
        ValidateAudience = true,
        ValidAudience = "your_audience",
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey("uaouorzszljaailjftpgamsbiqwftkteccbzanugsramhubiysnmcceullobxatdwrrdcxmxmtwkpbqgdbqiwhubdyrcjspsfgqiaprmrlybfvvhfwxogfptawxvlgnh"u8.ToArray())
    };
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine("Authentication failed: " + context.Exception.Message);
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            var issuer = context.SecurityToken.Issuer;
            Console.WriteLine($"Token validated successfully. Issuer: {issuer}");
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddSingleton<IAuthorizationHandler, DynamicAuthHandler>();

// Add services to the container.
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

// Map controllers so that routes are available
app.MapControllers(); 

app.Run();
