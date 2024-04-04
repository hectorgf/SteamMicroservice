using Microsoft.EntityFrameworkCore;
using SteamMicroservice.Model.Game;

namespace SteamMicroservice.Model.Configuration
{
    public class SteamDbContext : DbContext
    {
        public SteamDbContext(DbContextOptions<SteamDbContext> options) : base(options)
        {
        }

        public DbSet<SteamGame> Games {  get; set; }
        public DbSet<SteamRequirement> Requirements { get; set; }
        public DbSet<SteamDeveloper> Developers { get; set; }
        public DbSet<SteamPublisher> Publishers { get; set; }
        public DbSet<SteamCategory> Categories { get; set; }
        public DbSet<SteamGenre> Genres { get; set; }
        public DbSet<SteamScreenshot> Screenshots { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SteamGame>().ToTable("Games");
            modelBuilder.Entity<SteamGame>()
                .HasKey(game => game.Id);
            modelBuilder.Entity<SteamGame>()
                .HasMany(game => game.Requirements);
            modelBuilder.Entity<SteamGame>()
                .HasMany(game => game.Developers);
            modelBuilder.Entity<SteamGame>()
                .HasMany(game => game.Publishers);
            modelBuilder.Entity<SteamGame>()
                .OwnsOne(game => game.Price);
            modelBuilder.Entity<SteamGame>()
                .HasMany(game => game.Categories);
            modelBuilder.Entity<SteamGame>()
                .HasMany(game => game.Genres);
            modelBuilder.Entity<SteamGame>()
                .HasMany(game => game.Screenshots);
            modelBuilder.Entity<SteamGame>()
                .OwnsOne(game => game.ReleaseDate);

            modelBuilder.Entity<SteamRequirement>().ToTable("Requirements");
            modelBuilder.Entity<SteamRequirement>()
                .HasKey(requirement => requirement.Id);
            modelBuilder.Entity<SteamRequirement>()
                .HasOne(requirement => requirement.Game);

            modelBuilder.Entity<SteamDeveloper>().ToTable("Developers");
            modelBuilder.Entity<SteamDeveloper>()
                .HasKey(developer => developer.Id);
            modelBuilder.Entity<SteamDeveloper>()
                .HasMany(developer => developer.Games);

            modelBuilder.Entity<SteamPublisher>().ToTable("Publishers");
            modelBuilder.Entity<SteamPublisher>()
                .HasKey(publisher => publisher.Id);
            modelBuilder.Entity<SteamPublisher>()
                .HasMany(publisher => publisher.Games);

            modelBuilder.Entity<SteamCategory>().ToTable("Categories");
            modelBuilder.Entity<SteamCategory>()
                .HasKey(category => category.Id);
            modelBuilder.Entity<SteamCategory>()
                .HasMany(category => category.Games);

            modelBuilder.Entity<SteamGenre>().ToTable("Genres");
            modelBuilder.Entity<SteamGenre>()
                .HasKey(genre => genre.Id);
            modelBuilder.Entity<SteamGenre>()
                .HasMany(genre => genre.Games);

            modelBuilder.Entity<SteamScreenshot>().ToTable("Screenshots");
            modelBuilder.Entity<SteamScreenshot>()
                .HasKey(screenshot => screenshot.Id);
            modelBuilder.Entity<SteamScreenshot>()
                .HasOne(screenshot => screenshot.Game);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseLazyLoadingProxies();
    }
}