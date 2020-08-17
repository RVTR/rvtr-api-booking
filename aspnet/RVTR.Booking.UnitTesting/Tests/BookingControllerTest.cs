using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using RVTR.Booking.DataContext;
using RVTR.Booking.DataContext.Repositories;
using RVTR.Booking.ObjectModel.Models;
using RVTR.Booking.WebApi.Controllers;
using Xunit;

namespace RVTR.Booking.UnitTesting.Tests
{
  public class BookingControllerTest
  {
    private static readonly SqliteConnection _connection = new SqliteConnection("Data Source=:memory:");
    private static readonly DbContextOptions<BookingContext> _options = new DbContextOptionsBuilder<BookingContext>().UseSqlite(_connection).Options;
    private readonly BookingController _controller;
    private readonly ILogger<BookingController> _logger;
    private readonly UnitOfWork _unitOfWork;

    public BookingControllerTest()
    {
      var contextMock = new Mock<BookingContext>(_options);
      var loggerMock = new Mock<ILogger<BookingController>>();
      var repositoryMock = new Mock<Repository<BookingModel>>(new BookingContext(_options));
      var unitOfWorkMock = new Mock<UnitOfWork>(contextMock.Object);

      repositoryMock.Setup(m => m.DeleteAsync(0)).Throws(new Exception());
      repositoryMock.Setup(m => m.DeleteAsync(1)).Returns(Task.FromResult(1));
      repositoryMock.Setup(m => m.InsertAsync(It.IsAny<BookingModel>())).Returns(Task.FromResult<BookingModel>(null));
      repositoryMock.Setup(m => m.SelectAsync()).Returns(Task.FromResult<IEnumerable<BookingModel>>(null));
      repositoryMock.Setup(m => m.SelectAsync(0)).Throws(new Exception());
      repositoryMock.Setup(m => m.SelectAsync(1)).Returns(Task.FromResult<BookingModel>(null));
      repositoryMock.Setup(m => m.Update(It.IsAny<BookingModel>()));
      unitOfWorkMock.Setup(m => m.Booking).Returns(repositoryMock.Object);

      _logger = loggerMock.Object;
      _unitOfWork = unitOfWorkMock.Object;
      _controller = new BookingController(_logger, _unitOfWork);
    }

    [Fact]
    public async void Test_Controller_Delete()
    {
      var resultFail = await _controller.Delete(0);
      var resultPass = await _controller.Delete(1);

      Assert.NotNull(resultFail);
      Assert.NotNull(resultPass);
    }

    [Fact]
    public async void Test_Controller_Get()
    {
      var resultMany = await _controller.Get();
      var resultAll = await _controller.Get(null, null);
      var resultBookingDates = _controller.Get(new DateTime(2020, 8, 16), new DateTime(2020, 8, 18));

      Assert.NotNull(resultMany);
    }
    
    [Fact]
    public async void Test_Controller_GetById()
    {
      var resultFail = await _controller.GetById(0);
      var resultOne = await _controller.GetById(1);

      Assert.NotNull(resultFail);
      Assert.NotNull(resultOne);
    }

    [Fact]
    public async void Test_Controller_Post()
    {
      var resultPass = await _controller.Post(new BookingModel());

      Assert.NotNull(resultPass);
    }

    [Fact]
    public async void Test_Controller_Put()
    {
      var resultPass = await _controller.Put(new BookingModel());

      Assert.NotNull(resultPass);
    }
  }
}
