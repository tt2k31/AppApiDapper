using NLog;
using NLog.Web;
using AppApiDapper.Models;
using AppApiDapper.Services;
using AppApiDapper.Services.Interface;
using AppApiDapper.Services.Repository;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    ConfigurationManager configuration = builder.Configuration;

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

    builder.Services.AddDbContext<MyDBContext>(o =>
        o.UseSqlServer(builder.Configuration.GetConnectionString("MyDb")));
    builder.Services.AddStackExchangeRedisCache(o =>
    {
        o.Configuration = configuration["RedisCacheUrl"];
    });
    builder.Services.Configure<Appsettings>
        (builder.Configuration.GetSection("AppSettings"));


    
    var k = configuration["AppSettings:SecretKey"];

    var secretKeyByte = Encoding.UTF8.GetBytes(k);
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        //tu cap token
                        ValidateIssuer = false,
                        ValidateAudience = false,

                        //Ky vao token
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secretKeyByte),
                        ClockSkew = TimeSpan.Zero
                    };
                });
    builder.Services.AddScoped<ILogin, LoginReporitory>();
    builder.Services.AddCors(o => o.AddPolicy(name: "allowCors",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        }));

    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();

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

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}
catch (Exception ex)
{
    // NLog: catch setup errors
    logger.Error(ex, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}