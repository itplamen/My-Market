namespace MyMarket.Web.Models.Ad
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using Comments;
    using Data.Models;
    using Infrastructure.Mapping;

    public class AdViewModel : IMapFrom<Ad>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string UrlPath { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }

        public int Visits { get; set; }

        public int Likes { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }

        public void CreateMappings(IConfiguration config)
        {
            config.CreateMap<Ad, AdViewModel>()
                .ForMember(x => x.UrlPath, opt => opt.MapFrom(x => x.MainImage.UrlPath))
                .ForMember(x => x.Category, opt => opt.MapFrom(x => x.Category.Name))
                .ForMember(x => x.Visits, opt => opt.MapFrom(x => x.Visits.Count))
                .ForMember(x => x.Likes, opt => opt.MapFrom(x => x.Likes.Count))
                .ForMember(x => x.Comments, opt => opt.MapFrom(x => x.Comments.OrderByDescending(y => y.CreatedOn)));
        }
    }
}