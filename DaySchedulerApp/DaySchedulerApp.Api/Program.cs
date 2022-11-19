using DaySchedulerApp.Api.Middleware;
using DaySchedulerApp.Application;
using DaySchedulerApp.Identity;
using DaySchedulerApp.Persistance;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.ConfigurationSection(builder.Configuration.GetSection("Serilog")));
//builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(builder.Configuration.GetSection("Serilog")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureRepositoryServices(builder.Configuration);
builder.Services.ConfigureIdentityServices(builder.Configuration);

builder.Services.AddScoped<ErrorHandlingMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
