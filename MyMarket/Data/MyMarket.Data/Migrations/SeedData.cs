namespace MyMarket.Data.Migrations
{
    using System.Collections.Generic;

    using Models;

    public class SeedData
    {
        private IMyMarketDbContext context;

        public SeedData(IMyMarketDbContext context)
        {
            this.context = context;

            this.Categories = new List<Category>();
            this.SeedCategories();

            this.Countries = new List<Country>();
            this.SeedCountries();

            this.Cities = new List<City>();
            this.SeedCities();
        }

        public List<Category> Categories { get; set; }

        public List<City> Cities { get; set; }

        public List<Country> Countries { get; set; }

        private void SeedCategories()
        {
            this.Categories.Add(new Category() { Name = "Vehicles" });
            this.Categories.Add(new Category() { Name = "Properties" });
            this.Categories.Add(new Category() { Name = "Electronics & Accessories" });
            this.Categories.Add(new Category() { Name = "Travels" });
            this.Categories.Add(new Category() { Name = "Home & Garden" });
            this.Categories.Add(new Category() { Name = "Animals" });
            this.Categories.Add(new Category() { Name = "Fashion & Beauty" });
            this.Categories.Add(new Category() { Name = "Kids & Babies" });
            this.Categories.Add(new Category() { Name = "Sport & Sporting Goods" });
            this.Categories.Add(new Category() { Name = "Hobbies, Music, Art & Books" });
            this.Categories.Add(new Category() { Name = "Jobs" });
            this.Categories.Add(new Category() { Name = "Business & Industrial" });
            this.Categories.Add(new Category() { Name = "Services" });
        }

        private void SeedCountries()
        {
            this.Countries.Add(new Country() { Name = "Bulgaria" });
            this.Countries.Add(new Country() { Name = "Spain" });
        }

        private void SeedCities()
        {
            this.Cities.Add(new City()
            {
                Name = "Sofia",
                Country = this.Countries[0]
            });
            this.Cities.Add(new City()
            {
                Name = "Varna",
                Country = this.Countries[0]
            });
            this.Cities.Add(new City()
            {
                Name = "Plovdiv",
                Country = this.Countries[0]
            });
            this.Cities.Add(new City()
            {
                Name = "Ruse",
                Country = this.Countries[0]
            });

            this.Cities.Add(new City()
            {
                Name = "Madrid",
                Country = this.Countries[1]
            });
        }
    }
}
