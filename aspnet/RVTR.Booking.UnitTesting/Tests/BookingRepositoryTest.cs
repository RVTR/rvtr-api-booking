using System;
using System.Linq;
using RVTR.Booking.DataContext;
using RVTR.Booking.DataContext.Repositories;
using Xunit;

namespace RVTR.Booking.UnitTesting.Tests
{
  public class BookingRepositoryTest : DataTest
  {
    [Fact]
    public async void Test_GetBookingsById()
    {
      using var ctx = new BookingContext(Options);
      var bookings = new BookingRepository(ctx);
      var actual = await bookings.GetByAccountId(1);

      Assert.NotEmpty(actual);
    }

    [Fact]
    public async void Test_Repository_GetBookingsAsync_ByDate()
    {
      using var ctx = new BookingContext(Options);
      var bookings = new BookingRepository(ctx);
      var actual = await bookings.GetBookingsByDatesAsync(DateTime.Now.Date, DateTime.Now.AddDays(3).Date);

      Assert.NotEmpty(actual);
    }

    [Fact]
    public async void Test_Repository_GetBookingsWithRentalsAsync_ByDates()
    {
      using var ctx = new BookingContext(Options);
      var bookings = new BookingRepository(ctx);
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
      var actual = await bookings.SelectAsync(1);

      Assert.NotEmpty(actual.Rentals);
    }
  }
}
