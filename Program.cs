using Microsoft.EntityFrameworkCore;
using StreamingApi.Api.Data;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Swashbuckle.AspNetCore.Annotations;

var builder = WebApplication.CreateBuilder(args);

// Configure port for Render
var port = Environment.GetEnvironmentVariable("PORT") ?? "5011";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Streaming API",
        Version = "v1.0",
        Description = "API REST para gerenciamento de conteúdo de streaming - PIM VIII",
        Contact = new OpenApiContact
        {
            Name = "Pedro Tardivo",
            Email = "pedrotardivo7@gmail.com"
        },
        License = new OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });

    // Incluir comentários XML
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }

    // Configurar esquemas de exemplo
    // c.EnableAnnotations(); // Comentado temporariamente
});
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();

// Add CORS for client access
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Streaming API v1");
    c.RoutePrefix = "swagger"; // Define a rota como /swagger
    c.DocumentTitle = "Streaming API Documentation";
    c.DefaultModelsExpandDepth(-1); // Esconder modelos por padrão
    c.DisplayRequestDuration();
});

// Enable CORS
app.UseCors("AllowAll");

// Ensure database is created on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

// Add health check endpoint
app.MapGet("/", () => new
{
    message = "Streaming API is running!",
    version = "v1.0",
    timestamp = DateTime.UtcNow,
    endpoints = new
    {
        swagger = "/swagger",
        api = "/api",
        health = "/health"
    }
}).WithName("HealthCheck").WithTags("Health");

// Add detailed health check
app.MapGet("/health", () => new
{
    status = "healthy",
    timestamp = DateTime.UtcNow,
    uptime = Environment.TickCount64,
    version = "1.0.0",
    environment = app.Environment.EnvironmentName,
    database = "connected"
}).WithName("DetailedHealthCheck").WithTags("Health");

app.Run();


