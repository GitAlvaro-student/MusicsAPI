using Microsoft.EntityFrameworkCore;
using MusicSoundAPI.ApplicationDbContext;
using MusicSoundAPI.Repositories.Artist;
using MusicSoundAPI.Repositories.Music;
using MusicSoundAPI.Services.Artist;
using MusicSoundAPI.Services.Music;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

app.UseAuthorization();

app.MapControllers();

app.Run();
