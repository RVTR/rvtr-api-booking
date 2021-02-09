using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RVTR.Booking.Domain.Interfaces;
using RVTR.Booking.Domain.Models;

namespace RVTR.Booking.Context.Repositories
{
  public class BookingRepository : Repository<BookingModel>, IBookingRepository
  {
    public BookingRepository(BookingContext bookingContext) : base(bookingContext) { }

    /// <summary>
    /// Find bookings where the checkIn/checkOut date range is
    /// intersected in any way by the specified checkIn/checkOut range
    /// </summary>
    public virtual async Task<IEnumerable<BookingModel>> GetBookingsByDatesAsync(DateTime checkIn, DateTime checkOut)
    {
      var bookings = await Db.Where(b =>
        (checkIn <= b.CheckIn && checkOut >= b.CheckIn) || // Intersects left
        (checkIn <= b.CheckOut && checkOut >= b.CheckOut) || // Intersects right
        (checkIn <= b.CheckIn && checkOut >= b.CheckOut) || // Intersects inner
        (checkIn >= b.CheckIn && checkOut <= b.CheckOut) // Intersects outer
      )
      .Include(x => x.Rentals)
      .Include(x => x.Guests)
      .ToListAsync();

      return bookings;
    }

    /// <summary>
    /// Selects all booking models and .incldudes the attached rental and guest lists
    /// </summary>
    /// <returns></returns>
    public override async Task<IEnumerable<BookingModel>> SelectAsync()
    {
      var bookings =
        await Db
        .Include(x => x.Rentals)
        .Include(x => x.Guests)
        .ToListAsync();
      return bookings;
    }

    /// <summary>
    /// Selects a booking model by id and .include the attached rental and guest lists
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public override async Task<BookingModel> SelectAsync(int id)
    {
      var booking =
        await Db
        .Include(x => x.Rentals)
        .Include(x => x.Guests)
        .Where(x => x.Id == id)
        .FirstOrDefaultAsync();
      return booking;
    }

    public virtual async Task<IEnumerable<BookingModel>> GetByAccountId(int id)
    {
      return await Db.Where(t => t.AccountId == id).ToListAsync();
    }
  }
}
