namespace MyMarket.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;

    using Common;
    using MyMarket.Data.Models;

    public interface IAdsService
    {
        int Add(Ad ad);

        Ad Get(int id);

        IQueryable<Ad> GetAsQueryable(int id);

        IQueryable<Ad> All();

        IQueryable<Ad> AllWithDeleted();

        IQueryable<Ad> Latest(int count = NativeConstants.TopAdsCount);

        IQueryable<Ad> MostLiked(int count = NativeConstants.TopAdsCount);

        Ad Update(int id, Ad ad);

        Ad Delete(int id);

        bool HardDelete(int id);

        IQueryable<Ad> Search(
            string searchWord, 
            IEnumerable<int> categoriesIds, 
            string sortBy, string sortType, 
            int page = NativeConstants.ADS_START_PAGE, 
            int adsPerPage = NativeConstants.AdsPerPage);
    }
}