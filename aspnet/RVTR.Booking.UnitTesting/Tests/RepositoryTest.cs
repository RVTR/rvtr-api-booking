using Microsoft.EntityFrameworkCore;
using RVTR.Booking.DataContext;
using RVTR.Booking.DataContext.Repositories;
using RVTR.Booking.ObjectModel.Models;
using Xunit;

namespace RVTR.Booking.UnitTesting.Tests
{
  public class RepositoryTest : DataTest
  {
    private readonly BookingModel _booking = new BookingModel { Id = 3, AccountId = 1, LodgingId = 1 };

    [Fact]
    public async void Test_Repository_DeleteAsync()
    {
      using var ctx = new BookingContext(Options);
      var bookings = new Repository<BookingModel>(ctx);
      var sut = await ctx.Bookings.FirstAsync();
      await bookings.DeleteAsync(1);


      Assert.DoesNotContain(sut, await ctx.Bookings.ToListAsync());
    }

    [Fact]
    public async void Test_Repository_InsertAsync()
    {
      using var ctx = new BookingContext(Options);
      var bookings = new Repository<BookingModel>(ctx);

      await bookings.InsertAsync(_booking);

      Assert.Contains(_booking, await ctx.Bookings.ToListAsync());
    }

    [Fact]
    public async void Test_Repository_SelectAsync()
    {
      using var ctx = new BookingContext(Options);
      var bookings = new Repository<BookingModel>(ctx);
      var actual = await bookings.SelectAsync();

      Assert.NotEmpty(actual);
    }

    [Fact]
    public async void Test_Repository_SelectAsync_ById()
    {
      using var ctx = new BookingContext(Options);
      var bookings = new Repository<BookingModel>(ctx);
      var actual = await bookings.SelectAsync(1);

      Assert.NotNull(actual);
    }

    [Fact]
    public async void Test_Repository_Update()
    {
      using var ctx = new BookingContext(Options);
      var bookings = new Repository<BookingModel>(ctx);
      var expected = await ctx.Bookings.FirstAsync();

      bookings.Update(expected);

      var actual = await ctx.Bookings.FirstAsync();

      Assert.Equal(expected, actual);
    }
  }
}
