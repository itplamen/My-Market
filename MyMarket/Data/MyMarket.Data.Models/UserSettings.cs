﻿namespace MyMarket.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using MyMarket.Common;

    [ComplexType]
    public class UserSettings
    {
        public UserSettings()
        {
            this.DateOfBirth = null;
        }

        [Column("ProfilePicture")]
        public byte[] ProfilePicture { get; set; }

        [Column("FirstName")]
        [MinLength(ValidationConstants.USER_NAME_MIN_LENGTH)]
        [MaxLength(ValidationConstants.USER_NAME_MAX_LENGTH)]
        public string FirstName { get; set; }

        [Column("LastName")]
        [MinLength(ValidationConstants.USER_NAME_MIN_LENGTH)]
        [MaxLength(ValidationConstants.USER_NAME_MAX_LENGTH)]
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

        [Column("LastLogin")]
        public DateTime LastLogin { get; set; }

        [Column("LastLogout")]
        public DateTime LastLogout { get; set; }
    }
}