global using PatrickAPI.Models;
global using PatrickAPI.Services.CharacterService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// Every controller that wants to inject the IcharacterService interface
// has to use the characterService class.
// Scoped objects are the same within a request, but different across different requests.
builder.Services.AddScoped<ICharacterService, CharacterService>();

// Transient objects are always different; a new instance is provided to
// every controller and every service.
// builder.Services.AddTransient<ICharacterService, CharacterService>();

//Singleton objects are the same for every object and every request.
// builder.Services.AddSingleton<ICharacterService, CharacterService>();

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
