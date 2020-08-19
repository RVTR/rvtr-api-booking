using Microsoft.EntityFrameworkCore;
using RVTR.Booking.ObjectModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RVTR.Booking.DataContext.Repositories
{
  public class BookingRepository : Repository<BookingModel>
  {
    public BookingRepository(BookingContext bookingContext) : base(bookingContext) { }

    /// <summary>
    /// Find bookings where the checkIn/checkOut date range is
    /// intersected in any way by the specified checkIn/checkOut range
    /// </summary>
    public virtual async Task<IEnumerable<BookingModel>> GetBookingsByDatesAsync(DateTime checkIn, DateTime checkOut)
    {
      var bookings = await _db.Where(b =>
        (checkIn <= b.CheckIn && checkOut >= b.CheckIn) || // Intersects left
        (checkIn <= b.CheckOut && checkOut >= b.CheckOut) || // Intersects right
        (checkIn <= b.CheckIn && checkOut >= b.CheckOut) || // Intersects inner
        (checkIn >= b.CheckIn && checkOut <= b.CheckOut) // Intersects outer
      ).ToListAsync();

      return bookings;
    }

    public virtual async Task<IEnumerable<BookingModel>> GetByAccountId(int id)
    {
      return await _db.Where(t => t.AccountId == id).ToListAsync();
    }
  }
}
