using System.Collections.Generic;
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
        Id = 3,
        AccountId = 1,
        LodgingId = 1,
        CheckIn = new System.DateTime(2020,08,21),
        CheckOut = new System.DateTime(2020,08,24)
        },
      new BookingModel
      {
        Id = 2,
        AccountId = 2,
        LodgingId = 2,
        CheckIn = new System.DateTime(2020,08,18),
        CheckOut = new System.DateTime(2020,08,21)
        });
      modelBuilder.Entity<GuestModel>().HasData(
          new GuestModel
          {
            Id = 1,
            BookingId = 2
          },
          new GuestModel
          {
            Id = 2,
            BookingId = 2
          },
          new GuestModel
          {
            Id = 3,
            BookingId = 3
          }
      );
      modelBuilder.Entity<RentalModel>().HasData(
          new RentalModel
          {
            Id = 1,
            BookingId = 2
          },
          new RentalModel
          {
            Id = 2,
            BookingId = 2
          },
          new RentalModel
          {
            Id = 3,
            BookingId = 3
          }
      );
        
    }
  }
}
