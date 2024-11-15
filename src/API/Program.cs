using System.Data.SqlClient;
using System.Text.Json;
using BLL.GitHubPayloadStrategies;
using DefaultNamespace;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

var builder = WebApplication.CreateBuilder(args);

// Allow CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors();

app.MapPost("/github", async (HttpRequest request) =>
    {
        var payload = await request.ReadFromJsonAsync<dynamic>();
        var json = JsonSerializer.Serialize<Dictionary<string, object>>(payload);
        Console.WriteLine(json);
        GitHubPayloadRouter.Instance.Process(payload);
    })
    .Accepts<string>("application/json")
    .WithName("GitHubWebhookEndpoint")
    .WithOpenApi();

app.Run();