using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TesteTecnicoNTT.BFF.Settings;

var builder = WebApplication.CreateBuilder(args);

// Carregar configurações de autenticação
var authSettings = builder.Configuration.GetSection("AuthSettings").Get<AuthSettings>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"Falha na autenticação: {context.Exception}");
                return Task.CompletedTask;
            }
        };

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "TesteTecnicoNTT", // Nome do emissor
            ValidateAudience = true,
            ValidAudience = "TesteTecnicoNTTClients", // Audience esperado
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("1b667b328fd89c407220092c2c4d84a2930ddfa25d86733e18f70e88f72ba0a5")),
            //IssuerSigningKey = new SymmetricSecurityKey(
            //Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!)),
            ValidateIssuerSigningKey = true,
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddHttpClient("Gateway", client =>
{
    client.BaseAddress = new Uri("https://localhost:8080"); // Aponta para o API Gateway
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
