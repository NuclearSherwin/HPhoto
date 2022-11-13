using HPhoto.Authorization;
using HPhoto.Configs;
using HPhoto.Data;
using HPhoto.Model;
using HPhoto.Services;
using HPhoto.Services.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace HPhoto.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<DataContext>(options =>
        {
            options.UseSqlServer(AppSettings.ConnectionStrings,
                sqlOptions => sqlOptions.CommandTimeout(15000));
        });

        return serviceCollection;
    }
    public static IServiceCollection AddAutoMapper(this IServiceCollection serviceCollection)
    {
        var mapper = MappingConfig.RegisterMaps().CreateMapper();
        serviceCollection.AddSingleton(mapper);
        serviceCollection.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        return serviceCollection;
    }
    
    // For services
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ITagService, TagService>();
        serviceCollection.AddScoped<IPostService, PostService>();
        serviceCollection.AddScoped<ICommentService, CommentService>();
        serviceCollection.AddScoped<IUserService, UserService>();
        serviceCollection.AddScoped<IJwtUtils, JwtUtils>();

        return serviceCollection;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection service)
    {
        service.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web api khoi phi", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description =
                    "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
                    "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
                    "Example: \"Bearer 12345abcdef\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });

        });
            
        return service;
    }
}