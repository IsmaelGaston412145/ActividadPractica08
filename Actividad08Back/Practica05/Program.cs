using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Practica05.Models;
using Practica05.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORS",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// Add services to the container.
builder.Services.AddDbContext<turnos_db_2_7DbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionDefault")));

builder.Services.AddScoped<ITurnosRepository, TurnosRepository>();
builder.Services.AddScoped<IServiciosRepository, ServiciosRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CORS");

app.UseAuthorization();

app.MapControllers();

app.Run();
