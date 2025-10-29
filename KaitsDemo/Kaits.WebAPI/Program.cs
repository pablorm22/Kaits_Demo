using Kaits.Infrastructure;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// -----------------------------
// 1. Application + Infrastructure
// -----------------------------
builder.Services.AddMediatR(Assembly.Load("Kaits.Application"));
builder.Services.AddInfrastructure(builder.Configuration);

// -----------------------------
// 2. Configurar CORS
// -----------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // URL de tu frontend React
              .AllowAnyHeader()
              .AllowAnyMethod();
              // Si tu API usa cookies o auth JWT, puedes agregar: .AllowCredentials();
    });
});

// -----------------------------
// 3. Controllers + Swagger
// -----------------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// -----------------------------
// 4. Middleware
// -----------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Activar CORS antes de MapControllers
app.UseCors("AllowReactApp");

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
