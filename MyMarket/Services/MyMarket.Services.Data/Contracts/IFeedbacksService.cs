namespace MyMarket.Services.Data.Contracts
{
    using System.Linq;

    using MyMarket.Data.Models;

    public interface IFeedbacksService
    {
        int Add(Feedback feedback);

        Feedback Get(int id);

        IQueryable<Feedback> All();

        IQueryable<Feedback> AllWithDeleted();

        Feedback Update(int id, Feedback feedback);

        Feedback Delete(int id);

        bool HardDelete(int id);
    }
}
