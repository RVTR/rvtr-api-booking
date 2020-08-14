using System;
using System.Collections.Generic;
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
    ///
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="unitOfWork"></param>
    public BookingController(ILogger<BookingController> logger, UnitOfWork unitOfWork)
    {
      _logger = logger;
      _unitOfWork = unitOfWork;
    }

    /// <summary>
    ///
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
    ///
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
      return Ok(await _unitOfWork.Booking.SelectAsync());
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]

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
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("getByAccount/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> getByAccountId(int id)
    {
      try
      {
        return Ok(await _unitOfWork.bookingRepository.getByAccountId(id));
      }
      catch(Exception e)
      {
        return NotFound(e.Message);
      }

    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="booking"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status202Accepted)]

    public async Task<IActionResult> Post(BookingModel booking)
    {
      await _unitOfWork.Booking.InsertAsync(booking);
      await _unitOfWork.CommitAsync();

      return Accepted(booking);
    }

    /// <summary>
    ///
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


    /// <summary>
    /// 
    /// </summary>
    /// <param name="checkIn"></param>
    /// <param name="checkOut"></param>
    /// <returns></returns>
    [HttpGet("getByDate")]
    public async Task<IActionResult> getBookingsByDates([FromQuery]string checkIn, [FromQuery]string checkOut)
    {

      DateTime startDate = DateTime.Parse(checkIn);
      DateTime endDate = DateTime.Parse(checkOut);
      IEnumerable<BookingModel> bookings;

      try
      {
        bookings = await _unitOfWork.bookingRepository.getBookingsByDatesAsync(startDate, endDate);
      }
      catch(Exception e)
      {
        return (BadRequest(e));
      }

      return Ok(bookings);
       
    }
  }
}
