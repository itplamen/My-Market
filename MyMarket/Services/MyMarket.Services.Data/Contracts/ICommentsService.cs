namespace MyMarket.Services.Data.Contracts
{
    using System.Linq;

    using MyMarket.Data.Models;

    public interface ICommentsService
    {
        int Add(Comment comment);

        Comment Get(int id);

        IQueryable<Comment> All();

        Comment Update(int id, Comment comment);

        Comment Delete(int id);
    }
}