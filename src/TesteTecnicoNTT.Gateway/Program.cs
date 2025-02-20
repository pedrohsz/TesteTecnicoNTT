using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "TesteTecnicoNTT", // Mesmo issuer do BFF
            ValidAudience = "TesteTecnicoNTTClients", // Mesmo audience do BFF
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("1b667b328fd89c407220092c2c4d84a2930ddfa25d86733e18f70e88f72ba0a5")),
        };
    });

// Adiciona Ocelot e configurações
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
await app.UseOcelot();

app.Run();
