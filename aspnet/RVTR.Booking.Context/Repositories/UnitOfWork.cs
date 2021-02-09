using System;
using System.Threading.Tasks;
using RVTR.Booking.Domain.Interfaces;

namespace RVTR.Booking.Context.Repositories
{
  /// <summary>
  /// Represents the _UnitOfWork_ repository
  /// </summary>
  public class UnitOfWork : IUnitOfWork, IDisposable
  {
    private readonly BookingContext _context;
    private bool _disposedValue;

    public IBookingRepository Booking { get; }

    public UnitOfWork(BookingContext context)
    {
      _context = context;
      Booking = new BookingRepository(context);
    }

    /// <summary>
    /// Represents the _UnitOfWork_ `Commit` method
    /// </summary>
    /// <returns></returns>
    public async Task<int> CommitAsync() => await _context.SaveChangesAsync();

    protected virtual void Dispose(bool disposing)
    {
      if (!_disposedValue)
      {
        if (disposing)
        {
          _context.Dispose();
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
