using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RVTR.Booking.ObjectModel.Interfaces;
using RVTR.Booking.ObjectModel.Models;
using RVTR.Booking.WebApi.Controllers;
using Xunit;

namespace RVTR.Booking.UnitTesting.Tests
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
    public async void Test_Controller_Get()
    {
      IActionResult resultAll = await _controller.Get(null, null);
      Assert.IsAssignableFrom<OkObjectResult>(resultAll);
    }

    [Fact]
    public async void Test_Controller_Get_By_DateRange()
    {
      IActionResult resultBookingDates = await _controller.Get(new DateTime(2020, 8, 16), new DateTime(2020, 8, 18));
      Assert.IsAssignableFrom<OkObjectResult>(resultBookingDates);
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
      IActionResult resultPass = await _controller.Post(new BookingModel());
      Assert.IsAssignableFrom<CreatedAtActionResult>(resultPass);
    }

    [Fact]
    public async void Test_Controller_Put()
    {
      IActionResult resultPass = await _controller.Put(new BookingModel());
      Assert.IsAssignableFrom<NoContentResult>(resultPass);
    }
  }
}
