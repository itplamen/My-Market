namespace MyMarket.Web.Models.Ad
{
    using System.Collections.Generic;

    using Infrastructure.Mapping;
    
    public class AdsSearchResultViewModel : IMapFrom<AdViewModel>
    {
        public IEnumerable<AdViewModel> Ads { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }
    }
}