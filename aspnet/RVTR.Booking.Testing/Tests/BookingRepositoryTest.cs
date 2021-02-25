using System;
using System.Collections.Generic;
using System.Linq;
using RVTR.Booking.Context;
using RVTR.Booking.Context.Repositories;
using RVTR.Booking.Domain.Models;
using RVTR.Booking.Integrations;
using Xunit;

namespace RVTR.Booking.Testing.Tests
{
  public class BookingRepositoryTest : SqliteIntegration
  {
    [Fact]
    public async void Test_GetBookingsById()
    {
      using var ctx = new BookingContext(options);
      var bookings = new Repository<BookingModel>(ctx);

      ctx.Bookings.Add(
        new BookingModel()
        {
          EntityId = 0,
          AccountId = 1,
          LodgingId = 1,
          CheckIn = DateTime.Now.Date,
          CheckOut = DateTime.Now.AddDays(3).Date,
          Guests = new List<GuestModel>() { new GuestModel()
            {
              EntityId = 0,
              BookingModelId = 0,
              FirstName = "First Name User",
              LastName = "Last Name User",
              IsMinor = false
            }
          },
          Rentals = new List<RentalModel>() { new RentalModel() { LodgingRentalId = 1 } }
        }
      );
      await ctx.SaveChangesAsync();

      var actual = await bookings.SelectAsync(e => e.EntityId == 1);

      Assert.NotEmpty(actual);
    }

    [Fact]
    public async void Test_Repository_GetBookingsAsync_ByDate()
    {
      using var ctx = new BookingContext(options);
      var bookings = new Repository<BookingModel>(ctx);

      ctx.Bookings.Add(
        new BookingModel()
        {
          EntityId = 0,
          AccountId = 1,
          LodgingId = 1,
          CheckIn = DateTime.Now.Date,
          CheckOut = DateTime.Now.AddDays(3).Date,
          Guests = new List<GuestModel>() { new GuestModel()
            {
              EntityId = 0,
              BookingModelId = 0,
              FirstName = "First Name User",
              LastName = "Last Name User",
              IsMinor = false
            }
          },
          Rentals = new List<RentalModel>() { new RentalModel() { LodgingRentalId = 1 } }
        }
      );
      await ctx.SaveChangesAsync();

      var checkIn = DateTime.Now.Date;
      var checkOut = DateTime.Now.AddDays(3).Date;
      var actual = await bookings.SelectAsync(e =>
        (checkIn <= e.CheckIn && checkOut >= e.CheckIn) ||
        (checkIn <= e.CheckOut && checkOut >= e.CheckOut) ||
        (checkIn <= e.CheckIn && checkOut >= e.CheckOut) ||
        (checkIn >= e.CheckIn && checkOut <= e.CheckOut));

      Assert.NotEmpty(actual);
    }

    [Fact]
    public async void Test_Repository_GetBookingsWithRentalsAsync_ByDates()
    {
      using var ctx = new BookingContext(options);
      var bookings = new Repository<BookingModel>(ctx);

      ctx.Bookings.Add(
        new BookingModel()
        {
          EntityId = 0,
          AccountId = 1,
          LodgingId = 1,
          CheckIn = DateTime.Now.Date,
          CheckOut = DateTime.Now.AddDays(3).Date,
          Guests = new List<GuestModel>() { new GuestModel()
            {
              EntityId = 0,
              BookingModelId = 0,
              FirstName = "First Name User",
              LastName = "Last Name User",
              IsMinor = false
            }
          },
          Rentals = new List<RentalModel>() { new RentalModel() { LodgingRentalId = 1 } }
        }
      );
      await ctx.SaveChangesAsync();

      var checkIn = DateTime.Now.Date;
      var checkOut = DateTime.Now.AddDays(3).Date;
      var actual = await bookings.SelectAsync(e =>
        (checkIn <= e.CheckIn && checkOut >= e.CheckIn) ||
        (checkIn <= e.CheckOut && checkOut >= e.CheckOut) ||
        (checkIn <= e.CheckIn && checkOut >= e.CheckOut) ||
        (checkIn >= e.CheckIn && checkOut <= e.CheckOut));

      Assert.NotEmpty(actual.ToList()[0].Rentals);
    }

    /// <summary>
    /// Tests that a selecting all bookings db.includes the rental list as well
    /// </summary>
    [Fact]
    public async void Test_Repository_SelectAsync_HasRentals()
    {

      using var ctx = new BookingContext(options);
      var bookings = new Repository<BookingModel>(ctx);

      ctx.Bookings.Add(
        new BookingModel()
        {
          EntityId = 0,
          AccountId = 1,
          LodgingId = 1,
          CheckIn = DateTime.Now.Date,
          CheckOut = DateTime.Now.AddDays(3).Date,
          Guests = new List<GuestModel>() { new GuestModel()
            {
              EntityId = 0,
              BookingModelId = 0,
              FirstName = "First Name User",
              LastName = "Last Name User",
              IsMinor = false
            }
          },
          Rentals = new List<RentalModel>() { new RentalModel() { LodgingRentalId = 1 } }
        }
      );
      await ctx.SaveChangesAsync();

      var actual = await bookings.SelectAsync();

      Assert.NotEmpty(actual.ToList()[0].Rentals);
    }

    /// <summary>
    /// Tests that a selecting a single booking db.includes the rental list as well
    /// </summary>
    [Fact]
    public async void Test_Repository_SelectAsyncById_HasRentals()
    {

      using var ctx = new BookingContext(options);
      var bookings = new Repository<BookingModel>(ctx);

      ctx.Bookings.Add(
        new BookingModel()
        {
          EntityId = 0,
          AccountId = 1,
          LodgingId = 1,
          CheckIn = DateTime.Now.Date,
          CheckOut = DateTime.Now.AddDays(3).Date,
          Guests = new List<GuestModel>() { new GuestModel()
            {
              EntityId = 0,
              BookingModelId = 0,
              FirstName = "First Name User",
              LastName = "Last Name User",
              IsMinor = false
            }
          },
          Rentals = new List<RentalModel>() { new RentalModel() { LodgingRentalId = 1 } }
        }
      );
      await ctx.SaveChangesAsync();

      var actual = (await bookings.SelectAsync(e => e.EntityId == 1)).FirstOrDefault();

      Assert.NotEmpty(actual.Rentals);
    }
  }
}
