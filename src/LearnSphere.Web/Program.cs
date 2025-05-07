using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// 1. Razor Pages ve HttpClient
builder.Services.AddRazorPages();
builder.Services.AddHttpClient("ApiClient", client =>
{
    // API'nın dinlediği adrese burayı eşle:
    client.BaseAddress = new Uri("https://localhost:7047");
});

var app = builder.Build();

// 2. Statik dosyalar + yönlendirmeler
app.UseStaticFiles();
app.UseRouting();

app.MapRazorPages();

app.Run();
