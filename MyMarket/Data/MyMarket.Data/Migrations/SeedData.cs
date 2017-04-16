namespace MyMarket.Data.Migrations
{
    using System.Collections.Generic;

    using Models;
    using System.Drawing;
    using System.IO;

    public class SeedData
    {
        private IMyMarketDbContext context;

        public SeedData(IMyMarketDbContext context)
        {
            this.context = context;

            this.Categories = new List<Category>();
            this.SeedCategories();

            this.Ads = new List<Ad>();
            this.SeedAds();

            this.Countries = new List<Country>();
            this.SeedCountries();

            this.Cities = new List<City>();
            this.SeedCities();
        }

        public List<Category> Categories { get; set; }

        public List<Ad> Ads { get; set; }

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

        private void SeedAds()
        {
            //string mypath = Path.Combine(@"D:\", "Athens.jpg");
            //var image = System.Drawing.Image.FromFile(mypath);

            //byte[] arr = null;
            //using (var ms = new MemoryStream())
            //{
            //    image.Save(ms, image.RawFormat);
            //    arr = ms.ToArray();
            //}

            //var imagedd = new Data.Models.Image()
            //{
            //    UrlPath = Path.Combine(@"D:\", "Athens.jpg"),
                
            //};

            //for (int i = 1; i <= 10; i++)
            //{
            //    this.Ads.Add(new Ad()
            //    {
            //        Title = "Ad Title Test " + i,
            //        Description = "<p>Balkan Air launched a new route from Sofia (SOF) to Budapest (BUD), with a three times weekly " +
            //        "service beginning in October, as part of its winter 2017 schedule which will go on sale soon.</p>" +
            //        "<p>Balkan Air celebrated its new Sofia - Budapest route by releasing seats for sale at prices starting from " +
            //        "just &#8364;9.99 for travel in February and March. These low fare seats are available for booking until " +
            //        "midnight Monday, 30 January.</p>" +
            //        "<p><strong>Our Director of Air Service Development, Paul Winfield said:</strong></p>" +
            //        "<p><i>“It's great to be able to celebrate our first new route announcement of the year so early into 2017 and " +
            //        "for another airport in Italy to become linked with Liverpool later this year.</i></p>",
            //        Price = i,
            //        Category = this.Categories[0],
            //        MainImage = imagedd
            //    });
            //}
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
