using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RVTR.Booking.ObjectModel.Models;

namespace RVTR.Booking.DataContext.Repositories
{
  public interface IBookingRepository : IRepository<BookingModel>
  {
    Task<IEnumerable<BookingModel>> GetBookingsByDatesAsync(DateTime checkIn, DateTime checkOut);
    Task<IEnumerable<BookingModel>> GetByAccountId(int id);
  }
}
