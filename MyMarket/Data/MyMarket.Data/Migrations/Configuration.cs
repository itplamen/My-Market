namespace MyMarket.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Microsoft.AspNet.Identity.EntityFramework;

    using MyMarket.Common;

    public sealed class Configuration : DbMigrationsConfiguration<MyMarketDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(MyMarketDbContext context)
        {
            var seedData = new SeedData(context);

            if (!context.Roles.Any())
            {
                this.SeedRoles(context);
            }

            if (!context.Categories.Any())
            {
                context.Categories.AddOrUpdate(seedData.Categories.ToArray());
            }

            if (!context.Ads.Any())
            {
                context.Ads.AddOrUpdate(seedData.Ads.ToArray());
            }

            if (!context.Countries.Any())
            {
                context.Countries.AddOrUpdate(seedData.Countries.ToArray());
            }

            if (!context.Cities.Any())
            {
                context.Cities.AddOrUpdate(seedData.Cities.ToArray());
            }

            context.SaveChanges();
        }

        private void SeedRoles(MyMarketDbContext context)
        {
            foreach (var entity in context.Roles)
            {
                context.Roles.Remove(entity);
            }

            context.Roles.AddOrUpdate(new IdentityRole(UserRolesConstants.ADMINISTRATOR_ROLE));
            context.Roles.AddOrUpdate(new IdentityRole(UserRolesConstants.AUTHENTICATED_USER_ROLE));
        }
    }
}
