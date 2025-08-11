using Health.Motabea.Core.DTOs;
using Health.Motabea.Core;
using Health.Motabea.EF;
using Health.Motabea.EF.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<HealthAppContext>(options =>
    options.UseSqlServer(config.GetConnectionString("HealthClincDB")));

builder.Services.AddAutoMapper(typeof(HealthMap));
builder.Services.AddTransient(typeof(IHealthUnits), typeof(HealthUnit));

builder.Services.AddEndpointsApiExplorer();

var trqOrigins = "trqClinicOrig";
builder.Services.AddCors(options =>
    options.AddDefaultPolicy(p =>
        p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors(trqOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
