using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RVTR.Booking.DataContext;
using RVTR.Booking.DataContext.Repositories;
using Xunit;

namespace RVTR.Booking.UnitTesting.Tests
{
    public class UnitOfWorkTest
    {
        private static readonly SqliteConnection _connection = new SqliteConnection ("Data Source=:memory:");
        private static readonly DbContextOptions<BookingContext> _options = new DbContextOptionsBuilder<BookingContext> ().UseSqlite (_connection).Options;

        [Fact]
        public async void Test_UnitOfWork_CommitAsync ()
        {
            await _connection.OpenAsync ();

            try
            {
                using (var ctx = new BookingContext (_options))
                {
                    await ctx.Database.EnsureCreatedAsync ();
                }

                using (var ctx = new BookingContext (_options))
                {
                    var unitOfWork = new UnitOfWork (ctx);
                    var actual = await unitOfWork.CommitAsync ();

                    Assert.NotNull (unitOfWork.Booking);
                    Assert.Equal (0, actual);
                }
            }
            finally
            {
                await _connection.CloseAsync ();
            }
        }
    }
}
