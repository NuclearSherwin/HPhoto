using AutoMapper;
using HPhoto;
using HPhoto.Configs;
using HPhoto.Data;
using HPhoto.Extensions;
using HPhoto.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper()
    .AddDatabase()
    .AddAutoMapper()
    .AddServices()
    .AddSwagger();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddDbContext<DataContext>(options =>
// {
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
// });
// builder.Services.AddCors(options => options.AddPolicy(name: "Tags",
//     policy =>
//     {
//         policy.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
//     }
//     ));


// Config the app to read values from appsettings base on current environment value.
var configuration = new ConfigurationBuilder()
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("appsettings.json", false, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables().Build();

configuration.GetSection("AppSettings")
    .Get<AppSettings>(options => options.BindNonPublicProperties = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseCors("AllowAll");

// app.UseHttpsRedirection();

app.MapControllers();
app.UseStaticFiles();

// app.UseMiddleware<ErrorHandlerMiddleware>();
//             
// app.UseMiddleware<JwtMiddleware>();

app.Run();
