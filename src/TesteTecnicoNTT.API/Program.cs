using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using TesteTecnicoNTT.Application;
using TesteTecnicoNTT.Application.Pagamentos.Commands.Create;
using TesteTecnicoNTT.Common.Validation;
using TesteTecnicoNTT.Domain.Interfaces;
using TesteTecnicoNTT.Infrastructure;
using TesteTecnicoNTT.Infrastructure.Kafka;
using TesteTecnicoNTT.Infrastructure.Persistence.MongoDB;
using TesteTecnicoNTT.Infrastructure.Persistence.PostgresSQL;

var builder = WebApplication.CreateBuilder(args);

// Configurar Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console() // Exibir logs no console
    .CreateLogger();

// Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Teste Técnico NTT API",
        Version = "v1",
        Description = "API para gerenciamento de clientes e pagamentos.",
        Contact = new OpenApiContact
        {
            Name = "Suporte",
            Email = "suporte@teste.com",
            Url = new Uri("https://github.com/seu-repositorio") // Opcional
        }
    });
});

// Adicionando serviços da API
builder.Services.AddControllers();

// Adicionando Mediator
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// Adicionando a camada de aplicação e infraestrutura
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// Configuração do PostgreSQL e MongoDB
builder.Services.AddPostgreSqlPersistence(builder.Configuration);
builder.Services.AddMongoDbPersistence(builder.Configuration);

builder.Services.AddHostedService<KafkaConsumerService>();
builder.Services.AddScoped<IKafkaProducer, KafkaProducer>();
builder.Services.AddSingleton<IKafkaConsumer, KafkaConsumer>();

//builder.Services.AddAutoMapper(typeof(PagamentoProfile));
builder.Services.AddAutoMapper(typeof(PagamentoProfile), typeof(ClienteProfile));

// Registrar FluentValidation
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters(); // criar método estático
builder.Services.AddValidatorsFromAssemblyContaining<AdicionarPagamentoCommandValidator>();

var scope = builder.Services.BuildServiceProvider().CreateScope();
var mongoDbContext = scope.ServiceProvider.GetService<MongoDbContext>();

var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//    dbContext.Database.Migrate(); // Aplica todas as migrações pendentes automaticamente
//}

//if (app.Environment.IsDevelopment()) { }

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Teste Técnico NTT API v1");
    c.RoutePrefix = "swagger"; // Isso faz com que o Swagger seja carregado na raiz (http://localhost:5000/)
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

// Make the implicit Program class public so test projects can access it
public partial class Program { }
