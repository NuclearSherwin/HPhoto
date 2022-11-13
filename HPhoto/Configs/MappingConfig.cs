using AutoMapper;
using HPhoto.Dtos.CommentDto;
using HPhoto.Dtos.PostDto;
using HPhoto.Dtos.TagDto;
using HPhoto.Dtos.UserDto;
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
            
            // mapping for user
            config.CreateMap<User, AuthenticationResponse>();
            config.CreateMap<RegisterRequest, User>();
            config.CreateMap<UpdateRequest, User>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        switch (prop)
                        {
                            // ignore null && empty string properties
                            case null:
                            case string arg3 when string.IsNullOrEmpty(arg3):
                                return false;
                            default:
                                return true;
                        }
                    }
                    ));
        });
        return mappingConfig;
    }
}