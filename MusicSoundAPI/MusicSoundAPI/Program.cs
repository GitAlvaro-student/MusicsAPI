using Microsoft.EntityFrameworkCore;
using MonitoringLogs.Middleware;
using MonitoringLogs.Services;
using MusicSoundAPI.ApplicationDbContext;
using MusicSoundAPI.Repositories.Artist;
using MusicSoundAPI.Repositories.Music;
using MusicSoundAPI.Services.Artist;
using MusicSoundAPI.Services.Music;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Serilog Configuration
builder.Host.UseSerilog((context, configuration) =>
{
    configuration.WriteTo.Console()
        .WriteTo.File("logs/app-.txt", rollingInterval: RollingInterval.Day);
});

// Adding Oracle DB Context
var connString = builder.Configuration.GetConnectionString("DEV");
builder.Services.AddDbContext<ModelContext>(options =>
    options.UseOracle(connString));

// Adding Profile
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Adding Repositories
builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
builder.Services.AddScoped<IMusicRepository, MusicRepository>();

// Adding Services
builder.Services.AddScoped<IArtistService, ArtistService>();
builder.Services.AddScoped<IMusicService, MusicService>();
builder.Services.AddSingleton<IAzureService, AzureService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<Logging>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
