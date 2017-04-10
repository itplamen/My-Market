namespace MyMarket.Services.Data.Contracts
{
    using System.Linq;

    using MyMarket.Data.Models;

    public interface IAdsService
    {
        int Add(Ad ad);

        Ad Get(int id);

        IQueryable<Ad> All();

        IQueryable<Ad> AllWithDeleted();

        Ad Update(int id, Ad ad);

        Ad Delete(int id);

        bool HardDelete(int id);
    }
}
