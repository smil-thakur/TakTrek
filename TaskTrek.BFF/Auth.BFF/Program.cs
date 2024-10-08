using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using SQLConnector.Database;
using SQLConnector.Methods;

Env.Load("../.env");

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var connectionString = Environment.GetEnvironmentVariable("MYSQL_CONNECTION");

System.Console.WriteLine(connectionString);

builder.Services.AddDbContext<DataContext>(options => options.UseMySql(serverVersion: ServerVersion.AutoDetect(connectionString), connectionString: connectionString));

builder.Services.AddScoped<TestFetch>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();


app.Run();
