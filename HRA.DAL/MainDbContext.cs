using HRA.Common.Enums;
using HRA.DAL.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Linq;

namespace HRA.DAL
{
    public class MainDbContext : IdentityDbContext<User, Role, string>
    {
        public MainDbContext(DbContextOptions<MainDbContext> options)
               : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Group>().Property(e => e.ReferenceId).HasDefaultValueSql("NewId()");
            builder.Entity<Videos>().Property(e => e.ReferenceId).HasDefaultValueSql("NewId()");
            builder.Entity<Member>().Property(e => e.ReferenceId).HasDefaultValueSql("NewId()");

            builder.Entity<Group>()
               .HasMany(e => e.HraDetails)
               .WithOne(e => e.Group).IsRequired();

            builder.Entity<HraDetails>()
                .HasMany(e => e.Addresses)
                .WithOne(e => e.HraDetail).IsRequired()
                .HasForeignKey(e => e.HraDetailsId);

            builder.Entity<Videos>()
                .HasMany(e => e.UserVideos)
                .WithOne(e => e.Video);

            var roleList = Enum.GetNames(typeof(RoleType)).Select(role => new Role(role)).ToList();
            builder.Entity<Role>().HasData(roleList);

            var keysProperties = builder.Model.GetEntityTypes().Select(x => x.FindPrimaryKey()).SelectMany(x => x.Properties);
            foreach (var property in keysProperties)
            {
                property.ValueGenerated = ValueGenerated.OnAdd;
            }

            //SeedSampleVideos(builder);
        }

        public virtual DbSet<Address> Addresses { get; set; }

        public virtual DbSet<Group> Groups { get; set; }

        public virtual DbSet<HraDetails> HraDetails { get; set; }

        public virtual DbSet<UserVideos> UserVideos { get; set; }

        public virtual DbSet<Videos> Videos { get; set; }

        public virtual DbSet<Member> Members{ get; set; }

        private static void SeedSampleVideos(ModelBuilder builder)
        {
            builder.Entity<Videos>().HasData(
                new Videos { Id = 1, Title = "Sample Video 1", Url = "https://player.vimeo.com/video/347119375/", CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow },
                new Videos { Id = 2, Title = "Sample Video 2", Url = "https://player.vimeo.com/video/112866269/", CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow },
                new Videos { Id = 3, Title = "Sample Video 3", Url = "https://player.vimeo.com/video/6370469/", CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow },
                new Videos { Id = 4, Title = "Sample Video 4", Url = "https://player.vimeo.com/video/444571537/", CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow },
                new Videos { Id = 5, Title = "Sample Video 1", Url = "https://player.vimeo.com/video/347119375/", CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow },
                new Videos { Id = 6, Title = "Sample Video 2", Url = "https://player.vimeo.com/video/112866269/", CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow },
                new Videos { Id = 7, Title = "Sample Video 3", Url = "https://player.vimeo.com/video/6370469/", CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow },
                new Videos { Id = 8, Title = "Sample Video 4", Url = "https://player.vimeo.com/video/444571537/", CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow },
                new Videos { Id = 9, Title = "Sample Video 1", Url = "https://player.vimeo.com/video/347119375/", CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow },
                new Videos { Id = 10, Title = "Sample Video 2", Url = "https://player.vimeo.com/video/112866269/", CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow },
                new Videos { Id = 11, Title = "Sample Video 3", Url = "https://player.vimeo.com/video/6370469/", CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow },
                new Videos { Id = 12, Title = "Sample Video 4", Url = "https://player.vimeo.com/video/444571537/", CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow },
                new Videos { Id = 13, Title = "Sample Video 1", Url = "https://player.vimeo.com/video/347119375/", CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow },
                new Videos { Id = 14, Title = "Sample Video 2", Url = "https://player.vimeo.com/video/112866269/", CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow },
                new Videos { Id = 15, Title = "Sample Video 3", Url = "https://player.vimeo.com/video/6370469/", CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow },
                new Videos { Id = 16, Title = "Sample Video 4", Url = "https://player.vimeo.com/video/444571537/", CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow });
        }
    }
}
