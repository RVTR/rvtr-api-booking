
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RVTR.Booking.Domain.Interfaces;
using RVTR.Booking.Domain.Models;

namespace RVTR.Booking.Service.Controllers
{
  /// <summary>
  /// Booking controller
  /// </summary>
  [ApiController]
  [ApiVersion("0.0")]
  [EnableCors("Public")]
  [Route("rest/booking/{version:apiVersion}/[controller]")]
  public class BookingController : ControllerBase
  {
    private readonly ILogger<BookingController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Constructor of the booking controller.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="unitOfWork"></param>
    public BookingController(ILogger<BookingController> logger, IUnitOfWork unitOfWork)
    {
      _logger = logger;
      _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Takes in two dates and retrieves bookings between the two dates,
    /// returns all bookings if no checkin/checkout date specified.
    /// NEEDS TO BE DEPRICATED IN A LATER ISSUE
    /// </summary>
    /// <param name="checkIn"></param>
    /// <param name="checkOut"></param>
    /// <returns>List of bookings between date range</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BookingModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByDates(DateTime? checkIn, DateTime? checkOut)
    {
      _logger.LogDebug("Getting a booking between dates...");
      DateTime todaysDate = DateTime.Now.Date;
      if (checkIn != null && checkOut != null)
      {
        // Date range sanity check
        if (checkIn >= checkOut)
        {
          _logger.LogInformation($"Check In Date cannot be later than or equal to Check Out Date.");
          return BadRequest();
        }
        // Cannot time travel (yet) sanity check
        else if (checkIn < todaysDate)
        {
          _logger.LogInformation($"Check In Date cannot be earlier than Today's Date.");
          return BadRequest();
        }
        // Good Request
        var bookings = await _unitOfWork.Booking.GetBookingsByDatesAsync((DateTime)checkIn, (DateTime)checkOut);
        return Ok(bookings);
      }
      else if (checkIn == null && checkOut == null)
      {
       // _logger.LogInformation($"Returning bookings with null Check In and Check Out");
       return Ok(await _unitOfWork.Booking.SelectAsync(""));
      }
      else
      {
        _logger.LogWarning($"Failed to get bookings - invalid range given..");
        return BadRequest();
      }
    }

    /// <summary>
    /// Action method that returns a list of bookings associated with an account id.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet("Account/{input}")]
    [ProducesResponseType(typeof(IEnumerable<BookingModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(string input)
    {
      _logger.LogDebug("Getting a booking by either account email, or lodgingId..");
      var bookings = await _unitOfWork.Booking.SelectAsync(input);

      if (bookings.Count() == 0)
      {
        _logger.LogInformation($"Could not find bookings with either account email or lodgingId = {input}.");
        return NotFound(input);
      }
      else
      {
        _logger.LogInformation($"Succesfully found bookings using input = {input}.");
        return Ok(bookings);
      }
    }

    /// <summary>
    /// Action method that takes in a booking model and adds it into the database.
    /// </summary>
    /// <param name="booking"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(BookingModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> Post(BookingModel booking)
    {
      _logger.LogInformation($"Successfully added the booking with account email: '{booking.AccountEmail}' and lodgingID: {booking.LodgingId}'.");
      await _unitOfWork.Booking.InsertAsync(booking);
      await _unitOfWork.CommitAsync();

      return Ok(booking);
    }

    /// <summary>
    /// Action method that updates a booking resource in the database.
    /// </summary>
    /// <param name="booking"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Put(BookingModel booking)
    {
      _logger.LogInformation($"Successfully updated the booking with email: '{booking.AccountEmail}' and lodgingID: '{booking.LodgingId}'.");
      _unitOfWork.Booking.Update(booking);
      await _unitOfWork.CommitAsync();
      return NoContent();
    }
  }
}
