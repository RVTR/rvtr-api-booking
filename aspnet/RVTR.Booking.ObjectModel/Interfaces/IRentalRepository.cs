using RVTR.Booking.ObjectModel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RVTR.Booking.ObjectModel.Interfaces
{
  public interface IRentalRepository : IRepository<RentalModel>
  {
    Task<IEnumerable<RentalModel>> Get();
  }
}
