using CardGame_Heroes_REST_API.DataAccessObject;
using CardGame_Heroes_REST_API.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "CardGame_Heroes_REST_API", Version = "v1" });
});

builder.Services.AddDbContext<CardsDbContext>(options =>
{
    var serverVersion = new MySqlServerVersion(new Version(8, 0, 26));
    options.UseMySql(builder.Configuration.GetConnectionString("cardgame_heroes_cards"), serverVersion);
});

builder.Services.AddTransient<CardsRepository>();

builder.Services.AddMvc().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

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
