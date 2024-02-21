global using PatrickAPI.Models;
global using PatrickAPI.Services.CharacterService;
global using PatrickAPI.Dtos.Character;
global using AutoMapper;
global using Microsoft.EntityFrameworkCore;
global using PatrickAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// SQL server communication builder
// builder.Services.AddDbContext<DataContext>(
//     options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
// );

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<ICharacterService, CharacterService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
