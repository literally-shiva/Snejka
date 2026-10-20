using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Server;
using Server.Models;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();


var connectionString = builder.Configuration.GetConnectionString("ConnectionString")
    ?? throw new InvalidOperationException("ConnectionString не найден в конфигурации.");

builder.Services.AddDbContext<SnejkaDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
