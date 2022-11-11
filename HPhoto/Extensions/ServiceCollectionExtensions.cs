using HPhoto.Configs;

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
}