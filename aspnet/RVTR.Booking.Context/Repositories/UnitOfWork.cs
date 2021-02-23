using System;
using System.Threading.Tasks;
using RVTR.Booking.Domain.Interfaces;
using RVTR.Booking.Domain.Models;

namespace RVTR.Booking.Context.Repositories
{
  /// <summary>
  /// Represents the _UnitOfWork_ class
  /// </summary>
  public class UnitOfWork : IUnitOfWork
  {
    private readonly BookingContext _context;

    public IRepository<BookingModel> Booking { get; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="context"></param>
    public UnitOfWork(BookingContext context)
    {
      _context = context;
      Booking = new Repository<BookingModel>(context);
    }

    /// <summary>
    /// Represents the _UnitOfWork_ `CommitAsync` method
    /// </summary>
    /// <returns></returns>
    public async Task<int> CommitAsync() => await _context.SaveChangesAsync();
  }
}
