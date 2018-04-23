namespace MyMarket.Services.Data.Contracts
{
    using System.Linq;

    using MyMarket.Data.Models;

    public interface ICommentsService
    {
        int Add(Comment comment);

        IQueryable<Comment> All();
    }
}