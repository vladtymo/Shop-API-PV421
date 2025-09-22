using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string connStr = builder.Configuration.GetConnectionString("RemoteDb")
    ?? throw new Exception("No Connection String found.");

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<ShopDbContext>(options =>
    options.UseSqlServer(connStr));

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
