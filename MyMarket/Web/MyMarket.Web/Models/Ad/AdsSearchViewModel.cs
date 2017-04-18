namespace MyMarket.Web.Models.Ad
{
    using System.Collections.Generic;

    using Common;

    public class AdsSearchViewModel
    {
        public AdsSearchViewModel()
        {
            if (string.IsNullOrEmpty(this.SortBy))
            {
                this.SortBy = NativeConstants.AdsInitialOrderBy;
            }

            if (string.IsNullOrEmpty(this.SortType))
            {
                this.SortType = NativeConstants.AscendingSortAds;
            }
        }

        public string SearchWord { get; set; }

        public IEnumerable<int> ChosenCategoriesIds { get; set; }

        public string SortBy { get; set; }

        public string SortType { get; set; }
    }
}