using System;
using System.Threading.Tasks;

namespace RVTR.Booking.DataContext.Repositories
{
  /// <summary>
  /// Represents the _UnitOfWork_ repository
  /// </summary>
  public class UnitOfWork : IDisposable
  {
    private readonly BookingContext _context;
    public virtual BookingRepository Booking { get; }

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

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
      if (!disposed && disposing)
      {
        _context.Dispose();
      }
      disposed = true;
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }
  }
}
