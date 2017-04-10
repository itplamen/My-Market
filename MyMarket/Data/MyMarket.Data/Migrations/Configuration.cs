namespace MyMarket.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

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

            if (!context.Categories.Any())
            {
                context.Categories.AddOrUpdate(seedData.Categories.ToArray());
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
    }
}
