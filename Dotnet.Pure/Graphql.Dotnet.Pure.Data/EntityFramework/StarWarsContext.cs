using graphql.core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace graphql.data.EntityFramework
{
    public sealed class StarWarsContext : DbContext
    {
        public readonly ILogger Logger;
        private readonly bool _migrations;

        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Planet> Planets { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<CharacterFriend> CharacterFriends { get; set; }
        public DbSet<CharacterEpisode> CharacterEpisodes { get; set; }
        public DbSet<Droid> Droids { get; set; }
        public DbSet<Human> Humans { get; set; }

        public StarWarsContext()
        {
            _migrations = true;
        }

        public StarWarsContext(DbContextOptions options, ILogger<StarWarsContext> logger)
            : base(options)
        {
            Logger = logger;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_migrations)
            {
                // TODO: eliminate hardcode
                optionsBuilder.UseNpgsql("Host=localhost;Database=StarWars;Username=star;Password=wars;Enlist=false;");
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // https://docs.microsoft.com/en-us/ef/core/modeling/relationships
            // http://stackoverflow.com/questions/38520695/multiple-relationships-to-the-same-table-in-ef7core

            // episodes
            modelBuilder.Entity<Episode>().HasKey(c => c.Id);
            modelBuilder.Entity<Episode>().Property(e => e.Id).ValueGeneratedNever();

            // planets
            modelBuilder.Entity<Planet>().HasKey(c => c.Id);
            modelBuilder.Entity<Planet>().Property(e => e.Id).ValueGeneratedNever();

            // characters
            modelBuilder.Entity<Character>().HasKey(c => c.Id);
            modelBuilder.Entity<Character>().Property(e => e.Id).ValueGeneratedNever();

            // characters-friends
            modelBuilder.Entity<CharacterFriend>().HasKey(t => new {t.CharacterId, t.FriendId});

            modelBuilder.Entity<CharacterFriend>()
                .HasOne(cf => cf.Character)
                .WithMany(c => c.CharacterFriends)
                .HasForeignKey(cf => cf.CharacterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CharacterFriend>()
                .HasOne(cf => cf.Friend)
                .WithMany(t => t.FriendCharacters)
                .HasForeignKey(cf => cf.FriendId)
                .OnDelete(DeleteBehavior.Restrict);

            // characters-episodes
            modelBuilder.Entity<CharacterEpisode>().HasKey(t => new {t.CharacterId, t.EpisodeId});

            modelBuilder.Entity<CharacterEpisode>()
                .HasOne(cf => cf.Character)
                .WithMany(c => c.CharacterEpisodes)
                .HasForeignKey(cf => cf.CharacterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CharacterEpisode>()
                .HasOne(cf => cf.Episode)
                .WithMany(t => t.CharacterEpisodes)
                .HasForeignKey(cf => cf.EpisodeId)
                .OnDelete(DeleteBehavior.Restrict);

            // humans
            modelBuilder.Entity<Human>().HasOne(h => h.HomePlanet).WithMany(p => p.Humans);
        }
    }
}