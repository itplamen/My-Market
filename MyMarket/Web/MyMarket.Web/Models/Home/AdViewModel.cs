namespace MyMarket.Web.Models.Home
{
    using System;

    using AutoMapper;

    using Data.Models;
    using Infrastructure.Mapping;

    public class AdViewModel : IMapFrom<Ad>, IHaveCustomMappings
    {
        public DateTime CreatedOn { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public byte[] Picture { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }

        public int Visits { get; set; }

        public int Likes { get; set; }

        public int Comments { get; set; }

        public void CreateMappings(IMapperConfiguration config)
        {
            config.CreateMap<Ad, AdViewModel>()
                .ForMember(x => x.Category, opt => opt.MapFrom(x => x.Category.Name))
                .ForMember(x => x.Visits, opt => opt.MapFrom(x => x.Visits.Count))
                .ForMember(x => x.Likes, opt => opt.MapFrom(x => x.Likes.Count))
                .ForMember(x => x.Comments, opt => opt.MapFrom(x => x.Comments.Count));
        }
    }
}