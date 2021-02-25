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
  /// Represents the _BookingController_ class
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
    /// Action method for deleting a booking by booking id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
      var booking = (await _unitOfWork.Booking.SelectAsync(e => e.EntityId == id)).FirstOrDefault();

      if (booking == null)
      {
        return NotFound(id);
      }

      await _unitOfWork.Booking.DeleteAsync(id);
      await _unitOfWork.CommitAsync();

      return NoContent();
    }

    /// <summary>
    /// Takes in two dates and retrieves bookings between the two dates,
    /// returns all bookings if no checkin/checkout date specified.
    /// </summary>
    /// <param name="checkIn"></param>
    /// <param name="checkOut"></param>
    /// <returns>List of bookings between date range</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BookingModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(DateTime? checkIn, DateTime? checkOut)
    {
      var todaysDate = DateTime.Now.Date;

      if (checkIn == null || checkOut == null)
      {
        return Ok(await _unitOfWork.Booking.SelectAsync());
      }

      if (checkIn < DateTime.Now || checkIn >= checkOut)
      {
        return BadRequest();
      }

      var bookings = await _unitOfWork.Booking.SelectAsync(e =>
      (checkIn <= e.CheckIn && checkOut >= e.CheckIn) ||
      (checkIn <= e.CheckOut && checkOut >= e.CheckOut) ||
      (checkIn <= e.CheckIn && checkOut >= e.CheckOut) ||
      (checkIn >= e.CheckIn && checkOut <= e.CheckOut));

      return Ok(bookings);
    }

    /// <summary>
    /// Action method that returns a single booking by booking id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(BookingModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
      var bookings = await _unitOfWork.Booking.SelectAsync(e => e.EntityId == id);

      if (!bookings.Any())
      {
        return NotFound(id);
      }

      return Ok(bookings);
    }

    /// <summary>
    /// Action method that returns a list of bookings associated with an account id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("Account/{id}")]
    [ProducesResponseType(typeof(IEnumerable<BookingModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByAccountId(int id)
    {
      var bookings = await _unitOfWork.Booking.SelectAsync(e => e.AccountId == id);

      if (!bookings.Any())
      {
        return NotFound(id);
      }

      return Ok(bookings);
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
      booking.BookingNumber = Guid.NewGuid();

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
      _unitOfWork.Booking.Update(booking);

      await _unitOfWork.CommitAsync();

      return NoContent();
    }
  }
}
