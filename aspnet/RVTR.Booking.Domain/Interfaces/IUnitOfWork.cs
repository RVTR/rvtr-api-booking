using System.Threading.Tasks;
using RVTR.Booking.Domain.Models;

namespace RVTR.Booking.Domain.Interfaces
{
  /// <summary>
  ///
  /// </summary>
  public interface IUnitOfWork
  {
    IRepository<BookingModel> Booking { get; }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    Task<int> CommitAsync();
  }
}
