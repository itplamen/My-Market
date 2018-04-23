namespace MyMarket.Services.Data
{
    using System.Linq;

    using Bytes2you.Validation;

    using Contracts;
    using MyMarket.Data.Common;
    using MyMarket.Data.Models;
    
    public class CommentsService : ICommentsService
    {
        private readonly IDbRepository<Comment> commentsRepository;

        public CommentsService(IDbRepository<Comment> commentsRepository)
        {
            Guard.WhenArgument(commentsRepository, nameof(commentsRepository)).IsNull().Throw();

            this.commentsRepository = commentsRepository;
        }

        public int Add(Comment comment)
        {
            Guard.WhenArgument(comment, nameof(comment)).IsNull().Throw();

            this.commentsRepository.Add(comment);
            this.commentsRepository.Save();

            return comment.Id;
        }

        public IQueryable<Comment> All()
        {
            return this.commentsRepository.All();
        }
    }
}