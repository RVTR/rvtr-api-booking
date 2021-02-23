using Microsoft.EntityFrameworkCore;
using RVTR.Booking.Domain.Models;

namespace RVTR.Booking.Context
{
  /// <summary>
  /// Represents the _BookingContext_ class
  /// </summary>
  public class BookingContext : DbContext
  {
    public DbSet<BookingModel> Bookings { get; set; }

    public BookingContext(DbContextOptions<BookingContext> options) : base(options) { }

    /// <summary>
    ///
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<BookingModel>().HasKey(e => e.EntityId);
      modelBuilder.Entity<GuestModel>().HasKey(e => e.EntityId);
      modelBuilder.Entity<RentalModel>().HasKey(e => e.EntityId);
    }
  }
}
