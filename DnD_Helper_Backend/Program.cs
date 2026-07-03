using DnD_Helper_Backend.Data;
using DnD_Helper_Backend.Interfaces;
using DnD_Helper_Backend.Repositories;
using DnD_Helper_Backend.Services;
using DnD_Helper_Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DnDHelperDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();
//Servicios API
builder.Services.AddScoped<IPersonajeRepository, PersonajeRepository>();
builder.Services.AddScoped<IClaseReopository, ClaseRepository>();
builder.Services.AddScoped<IRazaReopository, RazaRepository>();
//Servicios Locales
builder.Services.AddScoped<IPersonajeCrearService, PersonajeCrearService>();
builder.Services.AddScoped<IPersonajeCalcService, PersonajeCalcService>();
builder.Services.AddScoped<ISkillService, SkillService>();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: "CorsPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
            .AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
