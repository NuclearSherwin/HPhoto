using AutoMapper;
using HPhoto.Dtos.CommentDto;
using HPhoto.Dtos.PostDto;
using HPhoto.Dtos.TagDto;
using HPhoto.Model;
using HPhoto.Services;

namespace HPhoto.Configs;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<TagUpsertRequest, Tag>().ReverseMap();
            config.CreateMap<PostUpsertRequest, Post>().ReverseMap();
            config.CreateMap<CommentUpsertRequest, Comment>().ReverseMap();
        });
        return mappingConfig;
    }
}