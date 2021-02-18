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
    BookingModel bookingModel = new BookingModel();

    public BookingRepositoryTest()
    {
      bookingModel.AccountId = 1;
      bookingModel.LodgingId = 1;
      bookingModel.CheckIn = DateTime.Now.Date;
      bookingModel.CheckOut = DateTime.Now.AddDays(3).Date;
      bookingModel.Guests = new List<GuestModel>() { new GuestModel() };
      bookingModel.Rentals = new List<RentalModel>() { new RentalModel() { LodgingRentalId = 1 } };
    }

    [Fact]
    public async void Test_GetBookingsById()
    {
      using var ctx = new BookingContext(Options);
      var bookings = new BookingRepository(ctx);

      ctx.Bookings.Add(bookingModel);
      await ctx.SaveChangesAsync();

      var actual = await bookings.GetByAccountId(1);

      Assert.NotEmpty(actual);
    }

    [Fact]
    public async void Test_Repository_GetBookingsAsync_ByDate()
    {
      using var ctx = new BookingContext(Options);
      var bookings = new BookingRepository(ctx);

      ctx.Bookings.Add(bookingModel);
      await ctx.SaveChangesAsync();

      var actual = await bookings.GetBookingsByDatesAsync(DateTime.Now.Date, DateTime.Now.AddDays(3).Date);

      Assert.NotEmpty(actual);
    }

    [Fact]
    public async void Test_Repository_GetBookingsWithRentalsAsync_ByDates()
    {
      using var ctx = new BookingContext(Options);
      var bookings = new BookingRepository(ctx);

      ctx.Bookings.Add(bookingModel);
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

     ctx.Bookings.Add(bookingModel);
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

      using var ctx = new BookingContext(Options);
      var bookings = new BookingRepository(ctx);

     ctx.Bookings.Add(bookingModel);
      await ctx.SaveChangesAsync();

      var actual = await bookings.SelectAsync(1);

      Assert.NotEmpty(actual.Rentals);
    }
  }
}
