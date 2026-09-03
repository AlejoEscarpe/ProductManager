using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ProductManager.Core.Interfaces;
using ProductManager.Infrastructure.Data;
using ProductManager.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Controladores y Swagger básico
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. DbContext y Servicios
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connectionString);
    // Evitar que la advertencia PendingModelChanges lance una excepción;
    // la registramos en lugar de convertirla en error en tiempo de ejecución.
    options.ConfigureWarnings(w => w.Log(RelationalEventId.PendingModelChangesWarning));
    // En desarrollo, habilitar logging de EF Core para ver SQL y datos sensibles (solo dev)
    if (builder.Environment.IsDevelopment())
    {
        options.EnableSensitiveDataLogging();
        options.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
    }
});

builder.Services.AddScoped<IProductService, ProductService>();

// 3. CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// 4. Swagger UI
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("AllowAngular");
app.UseAuthorization();
app.MapControllers();


// Aplicar migraciones automáticas al iniciar la aplicación
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    // Aplicar migraciones pendientes para garantizar que la BD tenga las tablas esperadas
    dbContext.Database.Migrate();
}

app.Run();