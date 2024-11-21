using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MVC_Project.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {

        public AppDbContext():base(){}
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Match> Matches { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-MHTAS3L\\SQLEXPRESS;Database=Booking;Integrated Security=true;Encrypt=false");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            



            // Define relationships  
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.User)
                .WithMany(u => u.Tickets)
                .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Match)
                .WithMany(m => m.Tickets)
                .HasForeignKey(t => t.MatchId);


            modelBuilder.Entity<Match>()
        .HasKey(m => m.Match_Id); 

           

        }

    }
}
