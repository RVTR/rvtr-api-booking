using System.Threading.Tasks;

namespace RVTR.Booking.ObjectModel.Interfaces
{
  public interface IUnitOfWork
  {
    IBookingRepository Booking { get; }

    Task<int> CommitAsync();
  }
}
