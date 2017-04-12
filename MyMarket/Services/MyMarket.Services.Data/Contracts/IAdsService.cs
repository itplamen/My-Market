namespace MyMarket.Services.Data.Contracts
{
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

        IQueryable<Ad> Latest(int count = ValidationConstants.TOP_ADS_COUNT);

        IQueryable<Ad> MostLiked(int count = ValidationConstants.TOP_ADS_COUNT);

        Ad Update(int id, Ad ad);

        Ad Delete(int id);

        bool HardDelete(int id);
    }
}
