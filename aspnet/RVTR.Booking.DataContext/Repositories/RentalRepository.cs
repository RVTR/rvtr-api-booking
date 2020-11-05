using System;
using System.Text;
using RVTR.Booking.ObjectModel.Interfaces;
using RVTR.Booking.ObjectModel.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace RVTR.Booking.DataContext.Repositories
{
  public class RentalRepository : Repository<RentalModel>, IRentalRepository
  {
    public RentalRepository(BookingContext bookingContext) : base(bookingContext) { }

    public virtual async Task<IEnumerable<RentalModel>> Get()
    {
      return await Db.ToListAsync();
    }
  }
}
