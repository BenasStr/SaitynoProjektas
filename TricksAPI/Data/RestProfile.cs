using AutoMapper;
using TricksAPI.Data.Dtos.Lessons;
using TricksAPI.Data.Dtos.Trick;
using TricksAPI.Data.Dtos.Comment;
using TricksAPI.Data.Dtos.Auth;
using TricksAPI.Data.Entities;

namespace TricksAPI.Data
{
    public class RestProfile : Profile
    {
        public RestProfile()
        {
            // Lesson Mappers
            CreateMap<Lesson, LessonDto>();
            CreateMap<CreateLessonDto, Lesson>();
            CreateMap<LessonDto, Lesson>();
            CreateMap<UpdateLessonDto, Lesson>();

            //Comment Mappers
            CreateMap<Comment, CommentDto>();
            CreateMap<CommentDto, Comment>();
            CreateMap<CreateCommentDto, Comment>();
            CreateMap<UpdateCommentDto, Comment>();

            //Trick Mappers
            CreateMap<Trick, TrickDto>();
            CreateMap<TrickDto, Trick>();

            //Auth Mappers
            CreateMap<RestUser, UserDto>();
        }
    }
}
