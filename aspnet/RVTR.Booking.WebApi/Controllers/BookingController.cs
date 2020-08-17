using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RVTR.Booking.DataContext.Repositories;
using RVTR.Booking.ObjectModel.Models;

namespace RVTR.Booking.WebApi.Controllers
{
  /// <summary>
  ///
  /// </summary>
  [ApiController]
  [ApiVersion("0.0")]
  [EnableCors("Public")]
  [Route("rest/booking/{version:apiVersion}/[controller]")]
  public class BookingController : ControllerBase
  {
    private readonly ILogger<BookingController> _logger;
    private readonly UnitOfWork _unitOfWork;

    /// <summary>
    /// Constructor of the booking controller.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="unitOfWork"></param>
    public BookingController(ILogger<BookingController> logger, UnitOfWork unitOfWork)
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
      try
      {
        await _unitOfWork.Booking.DeleteAsync(id);
        await _unitOfWork.CommitAsync();
        
        return Ok();
      }
      catch
      {
        return NotFound(id);
      }
    }

    /// <summary>
    /// Action method that returns a list of all the bookings.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
      return Ok(await _unitOfWork.Booking.SelectAsync());
    }

    /// <summary>
    /// Action method that returns a single booking by booking id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
      try
      {
        return Ok(await _unitOfWork.Booking.SelectAsync(id));
      }
      catch
      {
        return NotFound(id);
      }
    }

    /// <summary>
    /// Action method that returns a list of bookings associated with an account id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("Account/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByAccountId(int id)
    {
      try
      {
        return Ok(await _unitOfWork.bookingRepository.GetByAccountId(id));
      }
      catch(Exception e)
      {
        return NotFound(e.Message);
      }
    }

    /// <summary>
    /// Action method that takes in a booking model and adds it into the database.
    /// </summary>
    /// <param name="booking"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Post(BookingModel booking)
    {
      await _unitOfWork.Booking.InsertAsync(booking);
      await _unitOfWork.CommitAsync();

      return CreatedAtAction( 
        actionName: nameof(Get),
        routeValues: new { id = booking.Id },
        value: booking
      );
    }

    /// <summary>
    /// Action method that updates a booking resource in the database.
    /// </summary>
    /// <param name="booking"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public async Task<IActionResult> Put(BookingModel booking)
    {
      _unitOfWork.Booking.Update(booking);
      await _unitOfWork.CommitAsync();

      return Accepted(booking);
    }
  }
}
