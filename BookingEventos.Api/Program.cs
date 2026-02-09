using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using BookingEventos.Application.Interfaces;
using BookingEventos.Application.Services;
using BookingEventos.Domain.Ports;
using BookingEventos.Infrastructure.Repositories;
using BookingEventos.Infrastructure.Persistencia; // Asegúrate de que este sea el namespace de tu ApplicationDbContext
using Microsoft.EntityFrameworkCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// --- 1. SECCIÓN DE SERVICIOS (Antes del Build) ---

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// CONFIGURACIÓN DE BASE DE DATOS (MySQL con Docker)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// CONFIGURACIÓN DE SEGURIDAD JWT
var jwtKey = builder.Configuration.GetSection("Jwt:Key").Value;
var keyBytes = Encoding.UTF8.GetBytes(jwtKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
    };
});

builder.Services.AddAuthorization();

// --- REGISTRO DE SERVICIOS (Dependency Injection) ---
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>(); // Corregido: Interfaz -> Clase
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
// Registro de Servicios (Inyección de Dependencias)
builder.Services.AddScoped<IDjService, DjService>();
builder.Services.AddScoped<IBartenderService, BartenderService>();
builder.Services.AddScoped<ISalonService, SalonService>();
builder.Services.AddScoped<IReservaService, ReservaService>();

// --- REGISTRO DE REPOSITORIOS (Infraestructura) ---
// Esto le dice a la App: "Cuando alguien pida la Interfaz X, dale la Clase Y"
builder.Services.AddScoped<IDjRepository, DjRepository>();
builder.Services.AddScoped<IBartenderRepository, BartenderRepository>();
builder.Services.AddScoped<ISalonRepository, SalonRepository>();
builder.Services.AddScoped<IReservaRepository, ReservaRepository>();


// --- 2. CONSTRUCCIÓN DE LA APP ---
var app = builder.Build();

// --- 3. SECCIÓN DE MIDDLEWARES (Después del Build) ---

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// El orden es vital: ¿Quién eres? -> ¿Qué puedes hacer?
app.UseAuthentication();
app.UseAuthorization();

// --- 4. RUTAS (Endpoints) ---
app.UseMiddleware<BookingEventos.Api.Middlewares.ExceptionMiddleware>();
app.MapControllers();

app.Run();