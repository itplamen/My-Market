namespace MyMarket.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class Image : FileInfo
    {
        public int? AdId { get; set; }

        [InverseProperty("Images")]
        public virtual Ad Ad { get; set; }
    }
}
