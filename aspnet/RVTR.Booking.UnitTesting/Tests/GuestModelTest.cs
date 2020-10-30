using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RVTR.Booking.ObjectModel.Models;
using Xunit;

namespace RVTR.Booking.UnitTesting.Tests
{
  public class GuestModelTest
  {
    public static readonly IEnumerable<object[]> Guests = new List<object[]>
    {
      new object[]
      {
        new GuestModel()
        {
          Id = 0,
          BookingId = 0,
          Booking = new BookingModel()
        }
      }
    };

    [Theory]
    [MemberData(nameof(Guests))]
    public void Test_Create_GuestModel(GuestModel guest)
    {
      var validationContext = new ValidationContext(guest);
      var actual = Validator.TryValidateObject(guest, validationContext, null, true);

      Assert.True(actual);
    }

    [Fact]
    public void Test_Create_GuestModel_Fail()
    {
      GuestModel guest = new GuestModel()
      {
        Id = 0,
        BookingId = 0,
        Booking = null
      };
      var validationContext = new ValidationContext(guest);
      var actual = Validator.TryValidateObject(guest, validationContext, null, true);

      Assert.False(actual);
    }

    [Theory]
    [MemberData(nameof(Guests))]
    public void Test_Validate_GuestModel(GuestModel guest)
    {
      var validationContext = new ValidationContext(guest);
      Assert.Empty(guest.Validate(validationContext));
    }

    [Fact]
    public void Test_Validate_GuestModel_Fail()
    {
      GuestModel guest = new GuestModel()
      {
        Id = 0,
        BookingId = 0,
        Booking = null
      };
      var validationContext = new ValidationContext(guest);
      Assert.NotEmpty(guest.Validate(validationContext));
    }
  }
}
