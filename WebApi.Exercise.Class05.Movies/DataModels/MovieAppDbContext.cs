using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DataModels
{
    public class MovieAppDbContext : DbContext
    {
        public MovieAppDbContext(DbContextOptions options) : base(options) { }
 
        public DbSet<MovieDto> Movies { get; set; }
        public DbSet<UserDto> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<UserDto>()
            //    .HasMany(x => x.MovieList)
            //    .WithOne(x => x.User)
            //    .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<MovieDto>()
               .HasOne(x => x.User)
               .WithMany(x => x.MovieList)
               .HasForeignKey(x => x.UserId);

            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(Encoding.ASCII.GetBytes("Cece123"));
            var hashedPassword = Encoding.ASCII.GetString(md5data);

            modelBuilder.Entity<UserDto>()
                .HasData(new UserDto()
                {
                    Id = 1,
                    Username = "Cece",
                    Password = hashedPassword,
                    FirstName = "Cvetanka",
                    LastName = "Stojmilova",

                });
            modelBuilder.Entity<MovieDto>()
                .HasData(
                new MovieDto()
                {
                    Id = 1,
                    Title = "The Lion King",
                    Description = "The Lion King takes place in the Pride Lands of Africa, where a lion rules over the other animals as king",
                    Year = 2019,
                    Genre = 3,
                    UserId = 1
                },
                new MovieDto()
                {
                    Id = 2,
                    Title = "One upon a time in Holywood",
                    Description = "Quentin Tarantino's Once Upon a Time... in Hollywood visits 1969 Los Angeles, where everything is changing",
                    Year = 2019,
                    Genre = 1,
                    UserId = 1
                },
                  new MovieDto()
                  {
                      Id = 3,
                      Title = "Aladin",
                      Description = "A kind-hearted street urchin Aladdin vies for the love of the beautiful princess Jasmine, the princess of Agrabah. When he finds a magic lamp, he uses a genie's magic power to make himself a prince in order to marry her.",
                      Year = 2019,
                      Genre = 3,
                      UserId = 1
                  });

        }
    }
}
