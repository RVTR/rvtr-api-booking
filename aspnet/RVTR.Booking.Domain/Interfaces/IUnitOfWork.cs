using System.Threading.Tasks;

namespace RVTR.Booking.Domain.Interfaces
{
  public interface IUnitOfWork
  {
    IBookingRepository Booking { get; }

    Task<int> CommitAsync();
  }
}
