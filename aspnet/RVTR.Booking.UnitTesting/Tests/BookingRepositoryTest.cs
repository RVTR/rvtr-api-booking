using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RVTR.Booking.DataContext;
using RVTR.Booking.DataContext.Repositories;
using Xunit;

namespace RVTR.Booking.UnitTesting.Tests
{
  public class BookingRepositoryTest : IDisposable
  {
    private readonly SqliteConnection _connection;
    private readonly DbContextOptions<BookingContext> _options;
    private bool _disposedValue;

    public BookingRepositoryTest()
    {
      _connection = new SqliteConnection("Data Source=:memory:");
      _connection.Open();
      _options = new DbContextOptionsBuilder<BookingContext>().UseSqlite(_connection).Options;
      using var ctx = new BookingContext(_options);
      ctx.Database.EnsureCreated();
    }

    [Fact]
    public async void Test_GetBookingsById()
    {
      using var ctx = new BookingContext(_options);
      var bookings = new BookingRepository(ctx);
      var actual = await bookings.GetByAccountId(1);

      Assert.NotEmpty(actual);
    }

    [Fact]
    public async void Test_Repository_GetBookingsAsync_ByDate()
    {
      using var ctx = new BookingContext(_options);
      var bookings = new BookingRepository(ctx);
      var actual = await bookings.GetBookingsByDatesAsync(new DateTime(2020, 8, 17), new DateTime(2020, 8, 19));

      Assert.NotEmpty(actual);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!_disposedValue)
      {
        if (disposing)
        {
          _connection.Dispose();
        }
        _disposedValue = true;
      }
    }

    public void Dispose()
    {
      Dispose(disposing: true);
      GC.SuppressFinalize(this);
    }
  }
}
