using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RVTR.Booking.ObjectModel.Interfaces;
using RVTR.Booking.ObjectModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RVTR.Booking.WebApi.Controllers
{

  [ApiController]
  [ApiVersion("0.0")]
  [EnableCors("Public")]
  [Route("rest/booking/{version:apiVersion}/[controller]")]
  public class RentalController : ControllerBase
  {
    private readonly ILogger<RentalController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public RentalController(ILogger<RentalController> logger, IUnitOfWork unitOfWork)
    {
      _logger = logger;
      _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RentalModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get()
    {
      _logger.LogDebug("Getting a booking between dates...");
      var x = await _unitOfWork.Rental.Get();
      return Ok(x);
    }

  }
}
