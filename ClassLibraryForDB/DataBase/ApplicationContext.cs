using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ClassLibraryForDB.DataBase
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Result> Results { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public static ApplicationContext SetDBcon()
        {
            var builder = new ConfigurationBuilder();

            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            var options = optionsBuilder
                .UseSqlServer(connectionString)
                .Options;
            return new ApplicationContext(options);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Result>().HasData(
                new Result[]
                {
                new Result { Id=1, Input=2071, Answer=2017},
                new Result { Id=2, Input=9, Answer=-1},
                new Result { Id=3, Input=531,Answer=513}
                });
        }
    }

    public class Con
    {
        public static ApplicationContext SetDBcon()
        {
            var builder = new ConfigurationBuilder();

            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            var options = optionsBuilder
                .UseSqlServer(connectionString)
                .Options;
            return new ApplicationContext(options);

        }
    }
}