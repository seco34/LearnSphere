using LearnSphere.Data.Context;
using LearnSphere.Core.Services;
using LearnSphere.Services.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// 1. Servisleri kayıt et
builder.Services.AddControllers();                        
builder.Services.AddEndpointsApiExplorer();              
builder.Services.AddSwaggerGen();                        

builder.Services.AddDbContext<LearningDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<IHelloService, HelloService>();
// eğer CourseService vb. varsa onları da buraya ekleyin:
// builder.Services.AddScoped<ICourseService, CourseService>();

var app = builder.Build();

// 2. Orta katman (middleware) yapılandırması
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// 3. Controller rotalarını eşle
app.MapControllers();

app.Run();
