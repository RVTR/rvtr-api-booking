using System;
using System.Collections.Generic;
using System.Linq;
using RVTR.Booking.Context;
using RVTR.Booking.Context.Repositories;
using RVTR.Booking.Domain.Models;
using Xunit;

namespace RVTR.Booking.Testing.Tests
{
  public class BookingRepositoryTest : DataTest
  {
    [Fact]
    public async void Test_GetBookingsById()
    {
      using var ctx = new BookingContext(Options);
      var bookings = new BookingRepository(ctx);

      ctx.Bookings.Add(
        new BookingModel()
        {

          AccountEmail = "",
          EntityId = 0,
          LodgingId = 1,
          CheckIn = DateTime.Now.Date,
          CheckOut = DateTime.Now.AddDays(3).Date,
          Guests = new List<GuestModel>() { new GuestModel() },
          Rentals = new List<RentalModel>() { new RentalModel() { LodgingRentalId = 1 } }
        }
      );
      await ctx.SaveChangesAsync();

      var actual = await bookings.GetByAccountEmail("");

      Assert.NotEmpty(actual);
    }

    [Fact]
    public async void Test_Repository_GetBookingsAsync_ByDate()
    {
      using var ctx = new BookingContext(Options);
      var bookings = new BookingRepository(ctx);

      ctx.Bookings.Add(
        new BookingModel()
        {
          AccountEmail = "",
          EntityId = 0,
          LodgingId = 1,
          CheckIn = DateTime.Now.Date,
          CheckOut = DateTime.Now.AddDays(3).Date,
          Guests = new List<GuestModel>() { new GuestModel() },
          Rentals = new List<RentalModel>() { new RentalModel() { LodgingRentalId = 1 } }
        }
      );
      await ctx.SaveChangesAsync();

      var actual = await bookings.GetBookingsByDatesAsync(DateTime.Now.Date, DateTime.Now.AddDays(3).Date);

      Assert.NotEmpty(actual);
    }

    [Fact]
    public async void Test_Repository_GetBookingsWithRentalsAsync_ByDates()
    {
      using var ctx = new BookingContext(Options);
      var bookings = new BookingRepository(ctx);

      ctx.Bookings.Add(
        new BookingModel()
        {
          AccountEmail = "",
          EntityId = 0,
          LodgingId = 1,
          CheckIn = DateTime.Now.Date,
          CheckOut = DateTime.Now.AddDays(3).Date,
          Guests = new List<GuestModel>() { new GuestModel() },
          Rentals = new List<RentalModel>() { new RentalModel() { LodgingRentalId = 1 } }
        }
      );
      await ctx.SaveChangesAsync();

      var actual = await bookings.GetBookingsByDatesAsync(DateTime.Now.Date, DateTime.Now.AddDays(3).Date);

      Assert.NotEmpty(actual.ToList()[0].Rentals);
    }

    /// <summary>
    /// Tests that a selecting all bookings db.includes the rental list as well
    /// </summary>
    [Fact]
    public async void Test_Repository_SelectAsync_HasRentals()
    {

      using var ctx = new BookingContext(Options);
      var bookings = new BookingRepository(ctx);

      ctx.Bookings.Add(
        new BookingModel()
        {
          AccountEmail = "email",
          EntityId = 0,
          LodgingId = 1,
          CheckIn = DateTime.Now.Date,
          CheckOut = DateTime.Now.AddDays(3).Date,
          Guests = new List<GuestModel>() { new GuestModel() },
          Rentals = new List<RentalModel>() { new RentalModel() { LodgingRentalId = 1 } }

        }
      );
      await ctx.SaveChangesAsync();

      var actual = await bookings.SelectAsync("email");

      Assert.NotEmpty(actual);
    }

    /// <summary>
    /// Tests that a selecting a single booking db.includes the rental list as well
    /// </summary>
    [Fact]
    public async void Test_Repository_SelectAsync_HasNOTRentals()
    {

      using var ctx = new BookingContext(Options);
      var bookings = new BookingRepository(ctx);

      ctx.Bookings.Add(
        new BookingModel()
        {

          AccountEmail = "email",
          LodgingId = 1,
          CheckIn = DateTime.Now.Date,
          CheckOut = DateTime.Now.AddDays(3).Date,
          Guests = new List<GuestModel>() { new GuestModel() },

        }
      );
      await ctx.SaveChangesAsync();

      var actual = await bookings.SelectAsync("email");

      Assert.Empty(actual.First().Rentals);
    }

      /////////////////////////////

     [Fact]
    public async void Test_Repository_SelectAsync_ByEmailNotExists()
    {
      using var ctx = new BookingContext(Options);
      var bookings = new BookingRepository(ctx);

      ctx.Bookings.Add(
        new BookingModel()
        {
          AccountEmail = "Test1",
          EntityId = 0,
          LodgingId = 1,
          CheckIn = DateTime.Now.Date,
          CheckOut = DateTime.Now.AddDays(3).Date,
          Guests = new List<GuestModel>() { new GuestModel() },
          Rentals = new List<RentalModel>() { new RentalModel() { LodgingRentalId = 1 } }
        }
      );
      await ctx.SaveChangesAsync();

      IEnumerable<BookingModel> actual = await bookings.SelectAsync("NOTTHERE");

      Assert.Null(actual);
    }


    [Fact]
    public async void Test_Repository_SelectAsync_ByLodgingIdExists()
    {
      using var ctx = new BookingContext(Options);
      var bookings = new BookingRepository(ctx);

      ctx.Bookings.Add(
        new BookingModel()
        {
         AccountEmail = "",
          LodgingId = 1,
          CheckIn = DateTime.Now.Date,
          CheckOut = DateTime.Now.AddDays(3).Date,
          Guests = new List<GuestModel>() { new GuestModel() },
          Rentals = new List<RentalModel>() { new RentalModel() { LodgingRentalId = 1 } }
        }
      );
      await ctx.SaveChangesAsync();

      IEnumerable<BookingModel> actual = await bookings.SelectAsync("1");

      Assert.NotNull(actual.First());
    }

    [Fact]
    public async void Test_Repository_SelectAsync_ByLodgingIdNotExists()
    {
      using var ctx = new BookingContext(Options);
      var bookings = new BookingRepository(ctx);

      ctx.Bookings.Add(
        new BookingModel()
        {
         AccountEmail = "",
          LodgingId = 1,
          CheckIn = DateTime.Now.Date,
          CheckOut = DateTime.Now.AddDays(3).Date,
          Guests = new List<GuestModel>() { new GuestModel() },
          Rentals = new List<RentalModel>() { new RentalModel() { LodgingRentalId = 1 } }
        }
      );
      await ctx.SaveChangesAsync();

      var actual = await bookings.SelectAsync("2");

      Assert.Null(actual);
    }





  }
}
