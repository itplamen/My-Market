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

            context.SaveChanges();
        }
    }
}
