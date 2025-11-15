using IMCAPI.Data;
using IMCAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        builder => builder
            .WithOrigins(
                "http://localhost:3000",      // Frontend en Docker
                "http://localhost:80",        // Nginx local
                "http://imc-frontend",        // Nombre del contenedor
                "http://frontend"             // Alias del contenedor
            )
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Servicios de aplicación
builder.Services.AddScoped<CalculadoraIMCService>();

// Configuración de Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Calculadora IMC 2025",
        Version = "v1",
        Description = "API para cálculo del Índice de Masa Corporal"
    });
});

// Configuración de DbContext (si es necesario)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Habilitar CORS siempre (ajustar en producción según necesidades)
app.UseCors("AllowFrontend");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
