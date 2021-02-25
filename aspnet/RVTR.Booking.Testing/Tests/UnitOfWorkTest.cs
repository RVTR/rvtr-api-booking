using RVTR.Booking.Context;
using RVTR.Booking.Context.Repositories;
using RVTR.Booking.Integrations;
using Xunit;

namespace RVTR.Booking.Testing.Tests
{
  public class UnitOfWorkTest : SqliteIntegration
  {
    [Fact]
    public async void Test_UnitOfWork_CommitAsync()
    {
      using var ctx = new BookingContext(options);
      var unitOfWork = new UnitOfWork(ctx);
      var actual = await unitOfWork.CommitAsync();

      Assert.NotNull(unitOfWork.Booking);
      Assert.Equal(0, actual);
    }
  }
}
