global using BankingServer.Core;
global using BankingServer.Core.Entities;
global using BankingServer.Core.Exceptions;
global using BankingServer.Core.Data;


using BankingServer.Auth;
using BankingServer.Auth.Application.Services;
using BankingServer.Auth.Domain.Services;


var builder = WebApplication.CreateBuilder(args);


//Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Database
{
    DataContext.Register(builder);
}

//Dependency Injection
{
    AuthServiceRegistrar.Register(builder.Services);
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");
app.UseAuthorization();

app.MapControllers();

app.Run();