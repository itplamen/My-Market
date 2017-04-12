namespace MyMarket.Web.Models.Home
{
    using System.Collections.Generic;

    using Infrastructure.Mapping;

    public class SelectedAdsViewModel : IMapFrom<AdViewModel>
    {
        public IEnumerable<AdViewModel> LatestAds { get; set; }

        public IEnumerable<AdViewModel> MostLiked { get; set; }
    }
}