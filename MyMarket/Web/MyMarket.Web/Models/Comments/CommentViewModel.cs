namespace MyMarket.Web.Models.Comments
{
    using System;

    using AutoMapper;

    using MyMarket.Data.Models;
    using MyMarket.Web.Infrastructure.Mapping;

    public class CommentViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public string Content { get; set; }

        public string Username { get; set; }

        public DateTime Date { get; set; }

        public void CreateMappings(IConfiguration config)
        {
            config.CreateMap<Comment, CommentViewModel>()
                .ForMember(x => x.Username, opt => opt.MapFrom(x => x.User.UserName))
                .ForMember(x => x.Date, opt => opt.MapFrom(x => x.CreatedOn));
        }
    }
}