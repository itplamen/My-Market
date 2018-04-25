namespace MyMarket.Services.Data
{
    using System.Linq;

    using Bytes2you.Validation;

    using Contracts;
    using MyMarket.Common;
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

        public Comment Get(int id)
        {
            return this.commentsRepository.GetById(id);
        }

        public IQueryable<Comment> All()
        {
            return this.commentsRepository.All();
        }

        public Comment Update(int id, Comment comment)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.INVALID_ID).Throw();
            Guard.WhenArgument(comment, nameof(comment)).IsNull().Throw();

            Comment commentToUpdate = this.commentsRepository.GetById(id);

            if (commentToUpdate != null)
            {
                commentToUpdate.AdId = comment.AdId;
                commentToUpdate.Content = comment.Content;
                commentToUpdate.UserId = comment.UserId;
                commentToUpdate.CreatedOn = comment.CreatedOn;
                commentToUpdate.IsDeleted = comment.IsDeleted;
                commentToUpdate.DeletedOn = comment.DeletedOn;

                this.commentsRepository.Save();
            }

            return commentToUpdate;
        }

        public Comment Delete(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.INVALID_ID).Throw();

            Comment commentToDelete = this.commentsRepository.GetById(id);

            if (commentToDelete != null)
            {
                this.commentsRepository.Delete(commentToDelete);
                this.commentsRepository.Save();
            }

            return commentToDelete;
        }
    }
}