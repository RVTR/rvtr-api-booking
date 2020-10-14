using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RVTR.Booking.DataContext;
using RVTR.Booking.DataContext.Repositories;
using RVTR.Booking.ObjectModel.Models;
using Xunit;

namespace RVTR.Booking.UnitTesting.Tests
{
  public class RepositoryTest
  {
    private static readonly SqliteConnection _connection = new SqliteConnection("Data Source=:memory:");
    private static readonly DbContextOptions<BookingContext> _options = new DbContextOptionsBuilder<BookingContext>().UseSqlite(_connection).Options;
    private readonly BookingModel booking = new BookingModel() { Id = 3, AccountId = 1, LodgingId = 1 };

    [Fact]
    public async void Test_Repository_DeleteAsync()
    {
      await _connection.OpenAsync();

      try
      {
        using (var ctx = new BookingContext(_options))
        {
          await ctx.Database.EnsureCreatedAsync();
        }

        using (var ctx = new BookingContext(_options))
        {
          var bookings = new Repository<BookingModel>(ctx);
          var sut = await ctx.Bookings.FirstAsync();
          await bookings.DeleteAsync(1);
          await ctx.SaveChangesAsync();

          Assert.DoesNotContain(sut, await ctx.Bookings.ToListAsync());
        }


      }
      finally
      {
        _connection.Close();
      }
    }

    [Fact]
    public async void Test_Repository_InsertAsync()
    {
      await _connection.OpenAsync();

      try
      {
        using (var ctx = new BookingContext(_options))
        {
          await ctx.Database.EnsureCreatedAsync();
        }

        using (var ctx = new BookingContext(_options))
        {
          var bookings = new Repository<BookingModel>(ctx);

          await bookings.InsertAsync(booking);
          await ctx.SaveChangesAsync();

          Assert.Contains(booking, await ctx.Bookings.ToListAsync());
        }


      }
      finally
      {
        _connection.Close();
      }
    }

    [Fact]
    public async void Test_Repository_SelectAsync()
    {
      await _connection.OpenAsync();

      try
      {
        using (var ctx = new BookingContext(_options))
        {
          await ctx.Database.EnsureCreatedAsync();
        }

        using (var ctx = new BookingContext(_options))
        {
          var bookings = new Repository<BookingModel>(ctx);
          var actual = await bookings.SelectAsync();

          Assert.NotEmpty(actual);
        }

      }
      finally
      {
        _connection.Close();
      }
    }

    [Fact]
    public async void Test_Repository_SelectAsync_ById()
    {
      await _connection.OpenAsync();

      try
      {
        using (var ctx = new BookingContext(_options))
        {
          await ctx.Database.EnsureCreatedAsync();
        }

        using (var ctx = new BookingContext(_options))
        {
          var bookings = new Repository<BookingModel>(ctx);
          var actual = await bookings.SelectAsync(1);

          Assert.NotNull(actual);
        }

      }
      finally
      {
        _connection.Close();
      }
    }

    [Fact]
    public async void Test_Repository_Update()
    {
      await _connection.OpenAsync();

      try
      {
        using (var ctx = new BookingContext(_options))
        {
          await ctx.Database.EnsureCreatedAsync();
        }

        using (var ctx = new BookingContext(_options))
        {
          var bookings = new Repository<BookingModel>(ctx);
          var expected = await ctx.Bookings.FirstAsync();

          bookings.Update(expected);
          await ctx.SaveChangesAsync();

          var actual = await ctx.Bookings.FirstAsync();

          Assert.Equal(expected, actual);
        }
      }
      finally
      {
        _connection.Close();
      }
    }
  }
}
