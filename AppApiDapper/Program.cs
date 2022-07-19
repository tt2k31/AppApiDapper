using AppApiDapper.Data;
using AppApiDapper.Models;
using AppApiDapper.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddDbContext<MyDBContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("MyDb")));
builder.Services.Configure<Appsettings>
    (builder.Configuration.GetSection("AppSettings"));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
builder.Services.AddCors(o => o.AddPolicy(name: "allowCors",
    policy =>
    {
        policy.WithOrigins("http://localhost:4200")
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    }));
 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("allowCors");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
