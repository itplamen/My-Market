﻿namespace MyMarket.Common
{
    public static class NativeConstants
    {
        // Common
        public const int MinSearchTermLength = 3;
        public const string IMAGES_PATH = @"~/Content/Images/Posts/";

        // Ads
        public const int TopAdsCount = 8;
        public const int AdsPerPage = 15;
        public const int ADS_START_PAGE = 1;
        public const string AdsInitialOrderBy = "CreatedOn";
        public const string AscendingSortAds = "Ascending";
        public const string DescendingSortAds = "Descending";
    }
}
