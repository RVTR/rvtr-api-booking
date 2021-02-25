using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RVTR.Booking.Context;

namespace RVTR.Booking.Integrations
{
  /// <summary>
  ///
  /// </summary>
  public class SqliteIntegration
  {
    private readonly SqliteConnection _connection;
    protected readonly DbContextOptions<BookingContext> options;

    /// <summary>
    /// 
    /// </summary>
    protected SqliteIntegration()
    {
      _connection = new SqliteConnection("Data Source=:memory:");
      options = new DbContextOptionsBuilder<BookingContext>().UseSqlite(_connection).Options;

      _connection.Open();

      using (var ctx = new BookingContext(options))
      {
        ctx.Database.EnsureCreated();
      }
    }
  }
}
