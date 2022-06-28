using AutoMapper;
using PostService.Application.DTO_s.Post;
using PostService.Domain.Entities;

namespace PostService.Application.Profiles
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Post, GetAllPostDto>();
            CreateMap<PostCreateDto, Post>();
            CreateMap<Post, GetPostDto>();
        }
    }
}
