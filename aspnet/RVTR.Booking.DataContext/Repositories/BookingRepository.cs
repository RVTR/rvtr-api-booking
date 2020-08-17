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

    public BookingRepository(BookingContext bookingContext): base(bookingContext)
    {
      
    }

    public virtual async Task<IEnumerable<BookingModel>> getBookingsByDatesAsync(DateTime checkIn, DateTime checkOut)
    {
      var bookings = await _db.Where(b => b.CheckIn <= checkIn && b.Checkout >= checkOut).ToListAsync();

      return bookings;
    }

    public virtual async Task<IEnumerable<BookingModel>> getByAccountId(int id) => await _db.Where(t => t.AccountId == id).ToListAsync();
  }
}
