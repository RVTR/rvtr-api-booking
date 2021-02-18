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

      IEnumerable<BookingModel> bookings = new List<BookingModel> { new BookingModel() };
      var booking = new BookingModel();

      repositoryMock.Setup(m => m.DeleteAsync(0)).Returns(Task.CompletedTask);
      repositoryMock.Setup(m => m.DeleteAsync(1)).Returns(Task.CompletedTask);
      repositoryMock.Setup(m => m.InsertAsync(It.IsAny<BookingModel>())).Returns(Task.CompletedTask);
      repositoryMock.Setup(m => m.SelectAsync()).ReturnsAsync((IEnumerable<BookingModel>)null);
      repositoryMock.Setup(m => m.SelectAsync(0)).ReturnsAsync((BookingModel)null);
      repositoryMock.Setup(m => m.SelectAsync(1)).ReturnsAsync(booking);
      repositoryMock.Setup(m => m.Update(It.IsAny<BookingModel>()));
      repositoryMock.Setup(m => m.GetBookingsByDatesAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(bookings);
      repositoryMock.Setup(m => m.GetByAccountId(0)).ReturnsAsync(Enumerable.Empty<BookingModel>());
      repositoryMock.Setup(m => m.GetByAccountId(1)).ReturnsAsync(bookings);

      unitOfWorkMock.Setup(m => m.Booking).Returns(repositoryMock.Object);

      _logger = loggerMock.Object;
      _unitOfWork = unitOfWorkMock.Object;
      _controller = new BookingController(_logger, _unitOfWork);
    }

    [Fact]
    public async void Test_Controller_Delete()
    {
      IActionResult resultPass = await _controller.Delete(1);
      Assert.IsAssignableFrom<NoContentResult>(resultPass);
    }

    [Fact]
    public async void Test_Controller_Delete_NotFound()
    {
      IActionResult resultNotFound = await _controller.Delete(-1);
      Assert.IsAssignableFrom<NotFoundObjectResult>(resultNotFound);
    }

    [Fact]
    public async void Test_Controller_Get_Null_DateRange()
    {
      IActionResult resultAll = await _controller.Get(null, null);
      Assert.IsAssignableFrom<OkObjectResult>(resultAll);
    }

    [Fact]
    public async void Test_Controller_Get_By_DateRange()
    {
      // This test will only work up to January 1, 2021, because the current Get method returns a
      // BadRequestResult if the check in date is earlier than the current date (today's date)
      IActionResult resultBookingDates = await _controller.Get(new DateTime(2021, 12, 1), new DateTime(2021, 12, 2));
      Assert.IsAssignableFrom<OkObjectResult>(resultBookingDates);
    }

    [Fact]
    public async void Test_Controller_Get_By_Invalid_DateRange1()
    {
      // Returns Bad Request because the check in date is earlier than the current date (today's date)
      IActionResult resultBookingDates = await _controller.Get(new DateTime(2020, 10, 28), new DateTime(2020, 12, 31));
      Assert.IsAssignableFrom<BadRequestResult>(resultBookingDates);
    }

    [Fact]
    public async void Test_Controller_Get_By_Invalid_DateRange2()
    {
      // Returns Bad Request because the check out date is earlier than the check in date
      IActionResult resultBookingDates = await _controller.Get(new DateTime(2021, 1, 2), new DateTime(2021, 1, 1));
      Assert.IsAssignableFrom<BadRequestResult>(resultBookingDates);
    }

    [Fact]
    public async void Test_Controller_GetById()
    {
      IActionResult resultOne = await _controller.GetById(1);
      Assert.IsAssignableFrom<OkObjectResult>(resultOne);
    }

    [Fact]
    public async void Test_Controller_GetById_Fail()
    {
      IActionResult resultFail = await _controller.GetById(0);
      Assert.IsAssignableFrom<NotFoundObjectResult>(resultFail);
    }

    [Fact]
    public async void Test_Controller_Post()
    {
      BookingModel booking = new BookingModel()
      {
        Id = 0,
        AccountId = 0,
        LodgingId = 0,
        Guests = new List<GuestModel>(),
        Rentals = new List<RentalModel>(),
        BookingNumber = ""

      };
      IActionResult resultPass = await _controller.Post(booking);

      var sut = booking.BookingNumber;
      Assert.IsType<string>(sut);
      Assert.NotNull(sut);
      Assert.NotEmpty(sut);
      Assert.IsAssignableFrom<OkObjectResult>(resultPass);

    }

    [Fact]
    public async void Test_Controller_Put()
    {
      BookingModel booking = new BookingModel()
      {
        Id = 0,
        AccountId = 0,
        LodgingId = 0,
        Guests = new List<GuestModel>(),
        Rentals = new List<RentalModel>()

      };
      IActionResult resultPass = await _controller.Put(booking);
      Assert.IsAssignableFrom<NoContentResult>(resultPass);
    }

    [Fact]
    public async void Test_Controller_GetByAccountId()
    {
      IActionResult resultOk = await _controller.GetByAccountId(1);
      Assert.IsAssignableFrom<OkObjectResult>(resultOk);
    }

    [Fact]
    public async void Test_Controller_GetByAccountId_NotFound()
    {
      IActionResult resultNotFound = await _controller.GetByAccountId(-10);
      Assert.IsAssignableFrom<NotFoundObjectResult>(resultNotFound);
    }
  }
}
