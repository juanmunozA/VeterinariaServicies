using Microsoft.EntityFrameworkCore;
using Veterinaria.DBcontext;
using Veterinaria.Repositorio;
using Veterinaria.Servicio;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<VeterinariaContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar controladores
builder.Services.AddControllers();

// Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();


app.UseAuthorization();


app.MapControllers();


app.Run();
