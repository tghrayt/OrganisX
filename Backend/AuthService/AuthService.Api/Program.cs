using AuthService.Api.Configurations;
using AuthService.Infrastructure.Configurations;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddDependencyInjection();
builder.Services.AddControllers();
builder.Services.AddOpenApi();



var app = builder.Build();


app.UseHttpsRedirection();
app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())    
{
    app.MapOpenApi();
    app.MapScalarApiReference("/scalar", options => options
        .WithTitle("Organix")
        .WithTheme(ScalarTheme.Saturn)
        .WithDarkMode());
}

app.Run();
