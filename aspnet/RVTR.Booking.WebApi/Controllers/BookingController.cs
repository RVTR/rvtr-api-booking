using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RVTR.Booking.DataContext.Repositories;
using RVTR.Booking.ObjectModel.Models;

namespace RVTR.Booking.WebApi.Controllers
{
  [ApiController]
  [EnableCors()]
  [Route("[controller]")]
  public class BookingController : ControllerBase
  {
    private readonly ILogger<BookingController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public BookingController(ILogger<BookingController> logger, IUnitOfWork unitOfWork)
    {
      _logger = logger;
      _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<Reservation> Get()
    {
      return await Task.FromResult<Reservation>(new Reservation());

      //if(ModelState.IsValid)
    }
    
    [HttpPost]
    public async Task<BookingModel> Post(BookingModel book) {
      return await Task.FromResult<BookingModel>(book);
    }
  }
}
