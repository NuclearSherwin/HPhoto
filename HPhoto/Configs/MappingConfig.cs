using AutoMapper;
using HPhoto.Dtos.TagDto;
using HPhoto.Model;

namespace HPhoto.Configs;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<TagUpsertRequest, Tag>().ReverseMap();
        });
        return mappingConfig;
    }
}