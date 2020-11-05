using Microsoft.EntityFrameworkCore;
using RVTR.Booking.ObjectModel.Models;

namespace RVTR.Booking.DataContext
{
  /// <summary>
  /// Represents the _Booking_ context
  /// </summary>
  public class BookingContext : DbContext
  {
    public DbSet<BookingModel> Bookings { get; set; }

    public BookingContext(DbContextOptions<BookingContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<BookingModel>().HasKey(e => e.Id);
      modelBuilder.Entity<GuestModel>().HasKey(e => e.Id);
      modelBuilder.Entity<RentalModel>().HasKey(e => e.Id);
      modelBuilder.Entity<BookingModel>().HasData(
      new BookingModel
      {
        Id = 1,
        AccountId = 1,
        LodgingId = 2,
        CheckIn = new System.DateTime(2020, 08, 18),
        CheckOut = new System.DateTime(2020, 08, 21)
      });
      modelBuilder.Entity<GuestModel>().HasData(
          new GuestModel
          {
            Id = 1,
            BookingId = 1
          },
          new GuestModel
          {
            Id = 2,
            BookingId = 1
          },
          new GuestModel
          {
            Id = 3,
            BookingId = 1
          }
      );
      modelBuilder.Entity<RentalModel>().HasData(
          new RentalModel
          {
            Id = 1,
            BookingId = 1,
            LodgingRentalId = 5
          },
          new RentalModel
          {
            Id = 2,
            BookingId = 1,
            LodgingRentalId = 6
          },
          new RentalModel
          {
            Id = 3,
            BookingId = 1,
            LodgingRentalId = 7
          }
      );
    }
  }
}
