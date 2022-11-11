using HPhoto.Configs;
using HPhoto.Model;
using HPhoto.Services.IServices;

namespace HPhoto.Extensions;

public static class ServiceCollectionExtensions
{
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

        return serviceCollection;
    }
}