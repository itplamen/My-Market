namespace MyMarket.Web.Models.Category
{
    using AutoMapper;

    using Data.Models;
    using Infrastructure.Mapping;

    public class CategoryViewModel : IMapFrom<Category>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Ads { get; set; }

        public void CreateMappings(IConfiguration config)
        {
            config.CreateMap<Category, CategoryViewModel>()
                .ForMember(x => x.Ads, opt => opt.MapFrom(x => x.Ads.Count));
        }
    }
}