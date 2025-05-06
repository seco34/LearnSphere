using LearnSphere.Core.Services;
using LearnSphere.Services.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// 1. Add services to the container:
builder.Services.AddControllers();                                    // <-- Controller desteği
builder.Services.AddOpenApi();                                        // <-- Swagger/OpenAPI
builder.Services.AddScoped<IHelloService, HelloService>();            // <-- DI kaydı

var app = builder.Build();

// 2. Configure the HTTP request pipeline:
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();                                                 // <-- Swagger UI
}

app.UseHttpsRedirection();

app.UseAuthorization();

// 3. Map controllers:
app.MapControllers();

// 4. (Opsiyonel) Mevcut WeatherForecast minimal API endpoint:
app.MapGet("/weatherforecast", () =>
{
    var summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild",
        "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();

    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

// 5. Record tipi (minimal API için):
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
