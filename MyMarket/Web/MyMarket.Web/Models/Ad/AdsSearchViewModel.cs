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
                this.SortBy = Constants.AdsInitialOrderBy;
            }

            if (string.IsNullOrEmpty(this.SortType))
            {
                this.SortType = Constants.AscendingSortAds;
            }
        }

        public string SearchWord { get; set; }

        public IEnumerable<int> ChosenCategoriesIds { get; set; }

        public string SortBy { get; set; }

        public string SortType { get; set; }

        public bool IsSearchTermValid => !string.IsNullOrWhiteSpace(this.SearchWord) && this.SearchWord.Length >= Constants.MinimumTermLength;

        public int MinimumSearchTermLength => Constants.MinimumTermLength;
    }
}