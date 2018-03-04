namespace MyMarket.Services.Data
{
    using System.Linq;

    using Bytes2you.Validation;

    using Common;
    using Contracts;
    using MyMarket.Data.Common;
    using MyMarket.Data.Models;

    public class FeedbacksService : IFeedbacksService
    {
        private readonly IDbRepository<Feedback> feedbacksRepository;

        public FeedbacksService(IDbRepository<Feedback> feedbacksRepository)
        {
            Guard.WhenArgument(feedbacksRepository, nameof(feedbacksRepository)).IsNull().Throw();

            this.feedbacksRepository = feedbacksRepository;
        }

        public int Add(Feedback feedback)
        {
            Guard.WhenArgument(feedback, nameof(feedback)).IsNull().Throw();

            this.feedbacksRepository.Add(feedback);
            this.feedbacksRepository.Save();

            return feedback.Id;
        }

        public Feedback Get(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.INVALID_ID).Throw();

            return this.feedbacksRepository.GetById(id);
        }

        public IQueryable<Feedback> GetAsQueryable(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.INVALID_ID).Throw();

            return this.feedbacksRepository.All().Where(f => f.Id == id);
        }

        public IQueryable<Feedback> All()
        {
            return this.feedbacksRepository.All();
        }

        public IQueryable<Feedback> AllWithDeleted()
        {
            return this.feedbacksRepository.AllWithDeleted();
        }

        public Feedback Update(int id, Feedback feedback)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.INVALID_ID).Throw();
            Guard.WhenArgument(feedback, nameof(feedback)).IsNull().Throw();

            var feedbackToUpdate = this.feedbacksRepository.GetById(id);

            if (feedbackToUpdate != null)
            {
                feedbackToUpdate.CreatedOn = feedback.CreatedOn;
                feedbackToUpdate.IsDeleted = feedback.IsDeleted;
                feedbackToUpdate.DeletedOn = feedback.DeletedOn;
                feedbackToUpdate.Name = feedback.Name;
                feedbackToUpdate.Email = feedback.Email;
                feedbackToUpdate.Content = feedback.Content;
                feedbackToUpdate.UserId = feedback.UserId;

                this.feedbacksRepository.Save();
            }

            return feedbackToUpdate;
        }

        public Feedback Delete(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.INVALID_ID).Throw();

            var feedbackToDelete = this.feedbacksRepository.GetById(id);

            if (feedbackToDelete != null)
            {
                this.feedbacksRepository.Delete(feedbackToDelete);
                this.feedbacksRepository.Save();
            }

            return feedbackToDelete;
        }

        public bool HardDelete(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(ValidationConstants.INVALID_ID).Throw();

            var feedbackToDelete = this.feedbacksRepository.GetById(id);

            if (feedbackToDelete != null)
            {
                this.feedbacksRepository.HardDelete(feedbackToDelete);
                this.feedbacksRepository.Save();

                return true;
            }

            return false;
        }
    }
}