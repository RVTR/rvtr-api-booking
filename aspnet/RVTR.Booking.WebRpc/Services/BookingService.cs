using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using RVTR.Booking.DataContext.Repositories;

namespace RVTR.Booking.WebRpc
{
  public class BookingService : Booking.BookingBase
  {
    private readonly ILogger<BookingService> _logger;
    private readonly UnitOfWork _unitOfWork;

    public BookingService (ILogger<BookingService> logger, UnitOfWork unitOfWork)
    {
      _logger = logger;
      _unitOfWork = unitOfWork;
    }

    public override Task<BookingResponse> Book (BookingRequest request, ServerCallContext context)
    {
      return Task.FromResult (new BookingResponse ()
      {
        Message = "Hello " + request.Name
      });
    }
  }
}
