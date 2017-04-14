namespace MyMarket.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using MyMarket.Common;

    [ComplexType]
    public class UserSettings
    {
        [Column("ProfilePicture")]
        public byte[] ProfilePicture { get; set; }

        [Column("FirstName")]
        [MinLength(ValidationConstants.NameMinLength)]
        [MaxLength(ValidationConstants.NameMaxLenght)]
        public string FirstName { get; set; }

        [Column("LastName")]
        [MinLength(ValidationConstants.NameMinLength)]
        [MaxLength(ValidationConstants.NameMaxLenght)]
        public string LastName { get; set; }

        [Column("DateOfBirth")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Column("Gender")]
        public Gender Gender { get; set; }

        [Column("Nationality")]
        public string Nationality { get; set; }

        [Column("FullAddress")]
        public string FullAddress { get; set; }

        [Column("LastLogin", TypeName = "DateTime2")]
        public DateTime LastLogin { get; set; }

        [Column("LastLogout", TypeName = "DateTime2")]
        public DateTime LastLogout { get; set; }
    }
}
