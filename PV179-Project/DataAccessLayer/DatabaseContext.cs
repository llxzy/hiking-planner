using DataAccessLayer.DataClasses;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Location>     Locations     { get; set; }
        public DbSet<Trip>         Trips         { get; set; }
        public DbSet<User>         Users         { get; set; }
        public DbSet<UserTrip>     UserTrips     { get; set; }
        public DbSet<TripLocation> TripLocations { get; set; }
        public DbSet<Review>       Reviews       { get; set; }
        public DbSet<Challenge>    Challenges    { get; set; }

        public DatabaseContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTrip>().HasKey(k => new { k.UserId, k.TripId });
            modelBuilder.Entity<UserTrip>()
                .HasOne<User>(trip => trip.User)
                .WithMany(u => u.Trips)
                .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<UserTrip>()
                .HasOne<Trip>(trip => trip.Trip)
                .WithMany(t => t.Participants)
                .HasForeignKey(a => a.TripId);
            
            
            modelBuilder.Entity<UserReviewVote>()
                .HasOne<User>(vote => vote.AssociatedUser)
                .WithMany(u => u.UserReviewVotes)
                .HasForeignKey(a => a.AssociatedUserId);
            
            modelBuilder.Entity<UserReviewVote>()
                .HasOne<Review>(vote => vote.AssociatedReview)
                .WithMany(u => u.UserReviewVotes)
                .HasForeignKey(a => a.AssociatedReviewId);
            
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //todo replace with remote db probably and put connection string into appsettings
            optionsBuilder.UseNpgsql(@"Host=localhost;Database=tripdb;Username=postgres;Password=postgres;Port=5432");
            base.OnConfiguring(optionsBuilder);
        }
    }
}