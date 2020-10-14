using System.Threading.Tasks;

namespace RVTR.Booking.DataContext.Repositories
{
  public interface IUnitOfWork
  {
    IBookingRepository Booking { get; }

    Task<int> CommitAsync();
    void Dispose();
  }
}
