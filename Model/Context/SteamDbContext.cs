using Microsoft.EntityFrameworkCore;
using SteamMicroservice.Model.Games;
using SteamMicroservice.Model.Users;

namespace SteamMicroservice.Model.Configuration
{
    public class SteamDbContext : DbContext
    {
        public SteamDbContext(DbContextOptions<SteamDbContext> options) : base(options)
        {
        }

        public DbSet<Game> Games {  get; set; }
        public DbSet<SteamRequirement> Requirements { get; set; }
        public DbSet<SteamDeveloper> Developers { get; set; }
        public DbSet<SteamPublisher> Publishers { get; set; }
        public DbSet<SteamCategory> Categories { get; set; }
        public DbSet<SteamGenre> Genres { get; set; }
        public DbSet<SteamScreenshot> Screenshots { get; set; }
        public DbSet<Player> Players { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().ToTable("Games");
            modelBuilder.Entity<Game>()
                .HasKey(game => game.Id);
            modelBuilder.Entity<Game>()
                .HasMany(game => game.Requirements);
            modelBuilder.Entity<Game>()
                .HasMany(game => game.Developers);
            modelBuilder.Entity<Game>()
                .HasMany(game => game.Publishers);
            modelBuilder.Entity<Game>()
                .OwnsOne(game => game.Price);
            modelBuilder.Entity<Game>()
                .HasMany(game => game.Categories);
            modelBuilder.Entity<Game>()
                .HasMany(game => game.Genres);
            modelBuilder.Entity<Game>()
                .HasMany(game => game.Screenshots);
            modelBuilder.Entity<Game>()
                .OwnsOne(game => game.ReleaseDate);
            modelBuilder.Entity<Game>()
                .HasMany(game => game.Players);

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

            modelBuilder.Entity<Player>().ToTable("Players");
            modelBuilder.Entity<Player>()
                .HasKey(player => player.Id);
            modelBuilder.Entity<Player>()
                .HasMany(player => player.Games);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseLazyLoadingProxies();
    }
}