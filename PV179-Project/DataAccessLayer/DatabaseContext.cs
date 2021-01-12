using DataAccessLayer.DataClasses;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataAccessLayer
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Location>     Locations         { get; set; }
        public DbSet<Trip>         Trips             { get; set; }
        public DbSet<User>         Users             { get; set; }
        public DbSet<UserTrip>     UserTrips         { get; set; }
        public DbSet<TripLocation> TripLocations     { get; set; }
        public DbSet<Review>       Reviews           { get; set; }
        public DbSet<Challenge>    Challenges        { get; set; }
        public DbSet<UserReviewVote> UserReviewVotes { get; set; }

        public DatabaseContext()
        {
            Database.EnsureCreated(); // errors for autofac setup
        }

        
        public DatabaseContext(DbContextOptions options) : base(options)
        {
            //Database.EnsureCreated();
        }

        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=172.26.2.97\SQLEXPRESS,1433;User ID=pv178project;Password=eiGhtdRagon178;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }*/

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

            modelBuilder.Entity<UserReviewVote>().HasKey(ut => new {ut.AssociatedUserId, ut.AssociatedReviewId});
            modelBuilder.Entity<UserReviewVote>()
                .HasOne<User>(vote => vote.AssociatedUser)
                .WithMany(u => u.UserReviewVotes)
                .HasForeignKey(a => a.AssociatedUserId);
            
            modelBuilder.Entity<UserReviewVote>()
                .HasOne<Review>(vote => vote.AssociatedReview)
                .WithMany(u => u.UserReviewVotes)
                .HasForeignKey(a => a.AssociatedReviewId);
            
            modelBuilder.Entity<Challenge>()
                .HasOne(c => c.User)
                .WithMany(u => u.Challenges)
                .IsRequired()
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Challenges)
                .WithOne(c => c.User);

            // testing
            // redo later
            // gets rid of migration create error
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            // fix for identity insert
            // check for errors while getting data out
            // add for all data classes
            modelBuilder.Entity<Location>().Property(l => l.Id).UseIdentityColumn();

            base.OnModelCreating(modelBuilder);
        }
    }
}