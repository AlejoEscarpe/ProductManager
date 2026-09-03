using Microsoft.EntityFrameworkCore;
using ProductManager.Core.Interfaces;
using ProductManager.Infrastructure.Data;
using ProductManager.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Agregar soporte para Controladores
builder.Services.AddControllers();

// 2. Configurar Swagger/OpenAPI para probar endpoints
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3. Registrar DbContext con la cadena de conexión a SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 4. Configurar CORS para permitir peticiones desde Angular
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

// 5. Configurar el pipeline HTTP en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 6. Activar la política CORS
app.UseCors("AllowAngular");

app.UseAuthorization();

// 7. Mapear los controladores de la API
app.MapControllers();

app.Run();