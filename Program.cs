using Microsoft.EntityFrameworkCore;
using Veterinaria.DBContext;
using Veterinaria.Repositorio;
using Veterinaria.Servicio;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<VeterinariaContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar controladores
builder.Services.AddControllers();

// Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar CORS para permitir llamadas desde el frontend (React en localhost:3000)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
        // Si vas a enviar cookies o credenciales desde el frontend, añade .AllowCredentials();
    });
});

//  Repositorios
builder.Services.AddScoped<ClienteRepositorio>();
builder.Services.AddScoped<MascotaRepositorio>();
builder.Services.AddScoped<RazaRepositorio>();
builder.Services.AddScoped<VeterinarioRepositorio>();
builder.Services.AddScoped<MedicamentoRepositorio>();
builder.Services.AddScoped<FormulaRepositorio>();
builder.Services.AddScoped<FormulaMedicamentoRepositorio>();
builder.Services.AddScoped<HistorialRepositorio>();

//  Servicios
builder.Services.AddScoped<ClienteServicio>();
builder.Services.AddScoped<MascotaServicio>();
builder.Services.AddScoped<RazaServicio>();
builder.Services.AddScoped<VeterinarioServicio>();
builder.Services.AddScoped<MedicamentoServicio>();
builder.Services.AddScoped<FormulaServicio>();
builder.Services.AddScoped<FormulaMedicamentoServicio>();
builder.Services.AddScoped<HistorialServicio>();

// Configuración de JWT
var key = Encoding.ASCII.GetBytes("TuClaveSecretaSuperSegura"); // Cambia esta clave por una más segura
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Usar CORS con la política definida
app.UseCors("AllowFrontend");

app.UseAuthentication(); // Agregar autenticación
app.UseAuthorization();  // Agregar autorización

app.MapControllers();

app.Run();
