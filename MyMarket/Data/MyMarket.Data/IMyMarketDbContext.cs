namespace MyMarket.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using Models;

    public interface IMyMarketDbContext : IDisposable
    {
        IDbSet<Ad> Ads { get; set; }

        IDbSet<Category> Categories { get; set; }

        IDbSet<City> Cities { get; set; }

        IDbSet<Comment> Comments { get; set; }

        IDbSet<CommentFlag> CommentFlags { get; set; }

        IDbSet<Country> Countries { get; set; }

        IDbSet<Feedback> Feedbacks { get; set; }

        IDbSet<Flag> Flags { get; set; }

        IDbSet<Image> Images { get; set; }

        IDbSet<Like> Likes { get; set; }

        IDbSet<Visit> Visits { get; set; }

        DbSet<TEntity> Set<TEntity>()
            where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity)
            where TEntity : class;

        int SaveChanges();
    }
}
