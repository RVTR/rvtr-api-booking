using RVTR.Booking.Context;
using RVTR.Booking.Context.Repositories;
using Xunit;

namespace RVTR.Booking.Testing.Tests
{
  public class UnitOfWorkTest : DataTest
  {
    [Fact]
    public async void Test_UnitOfWork_CommitAsync()
    {
      using var ctx = new BookingContext(Options);
      var unitOfWork = new UnitOfWork(ctx);
      var actual = await unitOfWork.CommitAsync();

      Assert.NotNull(unitOfWork.Booking);
      Assert.Equal(0, actual);
    }
  }
}
