namespace MyMarket.Services.Data.Contracts
{
    using System.Linq;

    using MyMarket.Data.Models;

    public interface ICategoriesService
    {
        int Add(Category category);

        Category Get(int id);

        IQueryable<Category> GetAsQueryable(int id);

        IQueryable<Category> All();

        IQueryable<Category> AllWithDeleted();

        Category Update(int id, Category category);

        Category Delete(int id);

        bool HardDelete(int id);
    }
}
