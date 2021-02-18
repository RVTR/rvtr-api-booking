using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RVTR.Booking.Domain.Interfaces;
using RVTR.Booking.Domain.Models;
using RVTR.Booking.Service.Controllers;
using Xunit;

namespace RVTR.Booking.Testing.Tests
{
  public class BookingControllerTest
  {
    private readonly BookingController _controller;
    private readonly ILogger<BookingController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public BookingControllerTest()
    {
      var loggerMock = new Mock<ILogger<BookingController>>();
      var repositoryMock = new Mock<IBookingRepository>();
      var unitOfWorkMock = new Mock<IUnitOfWork>();

      IEnumerable<BookingModel> bookings = new List<BookingModel> { new BookingModel() {
        AccountEmail = "Test@email.com"
      }};
      var booking = new BookingModel();

      repositoryMock.Setup(m => m.InsertAsync(It.IsAny<BookingModel>())).Returns(Task.CompletedTask);
      repositoryMock.Setup(m => m.Update(It.IsAny<BookingModel>()));
      repositoryMock.Setup(m => m.GetBookingsByDatesAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(bookings);
      unitOfWorkMock.Setup(m => m.Booking).Returns(repositoryMock.Object);
      unitOfWorkMock.Setup(m => m.Booking).Returns(repositoryMock.Object);

      _logger = loggerMock.Object;
      _unitOfWork = unitOfWorkMock.Object;
      _controller = new BookingController(_logger, _unitOfWork);
    }

    [Fact]
    public async void Test_Controller_Get_Null_DateRange()
    {
      IActionResult resultAll = await _controller.GetByDates(null, null);
      Assert.IsAssignableFrom<OkObjectResult>(resultAll);
    }

    [Fact]
    public async void Test_Controller_Get_By_DateRange()
    {
      // This test will only work up to January 1, 2021, because the current Get method returns a
      // BadRequestResult if the check in date is earlier than the current date (today's date)
      IActionResult resultBookingDates = await _controller.GetByDates(new DateTime(2021, 12, 1), new DateTime(2021, 12, 2));
      Assert.IsAssignableFrom<OkObjectResult>(resultBookingDates);
    }

    [Fact]
    public async void Test_Controller_Get_By_Invalid_DateRange1()
    {
      // Returns Bad Request because the check in date is earlier than the current date (today's date)
      IActionResult resultBookingDates = await _controller.GetByDates(new DateTime(2020, 10, 28), new DateTime(2020, 12, 31));
      Assert.IsAssignableFrom<BadRequestResult>(resultBookingDates);
    }

    [Fact]
    public async void Test_Controller_Get_By_Invalid_DateRange2()
    {
      // Returns Bad Request because the check out date is earlier than the check in date
      IActionResult resultBookingDates = await _controller.GetByDates(new DateTime(2021, 1, 2), new DateTime(2021, 1, 1));
      Assert.IsAssignableFrom<BadRequestResult>(resultBookingDates);
    }

    [Fact]
    public async void Test_Controller_Post()
    {
      BookingModel booking = new BookingModel()
      {
        Id = 0,
        AccountEmail = "",
        LodgingId = 0,
        Guests = new List<GuestModel>(),
        Rentals = new List<RentalModel>()

      };
      IActionResult resultPass = await _controller.Post(booking);
      Assert.IsAssignableFrom<OkObjectResult>(resultPass);
    }

    [Fact]
    public async void Test_Controller_Put()
    {
      BookingModel booking = new BookingModel()
      {
        Id = 0,
        AccountEmail = "",
        LodgingId = 0,
        Guests = new List<GuestModel>(),
        Rentals = new List<RentalModel>()

      };
      IActionResult resultPass = await _controller.Put(booking);
      Assert.IsAssignableFrom<NoContentResult>(resultPass);
    }

    [Fact]
    public async void Test_Controller_GetByAccountId_NotFound()
    {
      IActionResult resultNotFound = await _controller.Get("NotEmail");
      Assert.IsAssignableFrom<NotFoundObjectResult>(resultNotFound);
    }

  }
}
