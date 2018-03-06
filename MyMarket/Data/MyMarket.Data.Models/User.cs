namespace MyMarket.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using Common.Models;

    public class User : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public User()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.UserSettings = new UserSettings();
            this.Ads = new HashSet<Ad>();
            this.Comments = new HashSet<Comment>();
            this.Likes = new HashSet<Like>();
            this.Visits = new HashSet<Visit>();
            this.Flags = new HashSet<Flag>();
            this.Feedbacks = new HashSet<Feedback>();
        }

        public UserSettings UserSettings { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<Ad> Ads { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Like> Likes { get; set; }

        public virtual ICollection<Visit> Visits { get; set; }

        public virtual ICollection<Flag> Flags { get; set; }

        public virtual ICollection<Feedback> Feedbacks { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }
    }   
}