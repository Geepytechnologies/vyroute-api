using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using vyroute.Models;

namespace vyroute.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<Terminal> Terminals { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Transit> Transits { get; set; }

        public DbSet<Transporter> Transporters { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Booking>()
                .HasOne(b => b.Terminal)
                .WithMany(t => t.Bookings)
                .HasForeignKey(b => b.TerminalId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Booking>()
                    .HasOne(b => b.Transit)
                    .WithMany(t => t.Bookings)
                    .HasForeignKey(b => b.TransitId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Transit>()
                    .HasOne(b => b.Terminal)
                    .WithMany(t => t.Transits)
                    .HasForeignKey(b => b.TerminalId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Transit>()
                    .HasOne(b => b.Vehicle)
                    .WithMany(t => t.Transits)
                    .HasForeignKey(b => b.VehicleID)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Payment>()
                .HasOne(p => p.Booking)
                .WithOne(b => b.Payment)
                .HasForeignKey<Payment>(p => p.BookingID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Vehicle>()
                .HasOne(p => p.Transporter)
                .WithOne(b => b.Vehicle)
                .HasForeignKey<Vehicle>(p => p.TransporterId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Vehicle>()
                .HasOne(p => p.Terminal)
                .WithMany(b => b.Vehicles)
                .HasForeignKey(p => p.TerminalId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
            .HasIndex(u => u.PhoneNumber)
            .IsUnique();


        }
    }
}
