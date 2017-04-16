namespace MyMarket.Services.Data.Contracts
{
    using System.Linq;

    using MyMarket.Data.Models;

    public interface IImagesService
    {
        int Add(Image image);

        Image Get(int id);

        IQueryable<Image> GetAsQueryable(int id);

        IQueryable<Image> All();

        IQueryable<Image> AllWithDeleted();

        Image Update(int id, Image image);

        Image Delete(int id);

        bool HardDelete(int id);
    }
}
