using RVTR.Booking.DataContext;
using RVTR.Booking.DataContext.Repositories;
using Xunit;

namespace RVTR.Booking.UnitTesting.Tests
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
