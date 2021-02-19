using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RVTR.Booking.Domain.Models;
using Xunit;

namespace RVTR.Booking.Testing.Tests
{
  public class GuestModelTest
  {
    public static readonly IEnumerable<object[]> Guests = new List<object[]>
    {
      new object[]
      {
        new GuestModel()
        {
          EntityId = 0,
          BookingModelId = 0,
          FirstName = "First Name User",
          LastName = "Last Name User",
          IsMinor = false
        }
      }
    };

    [Theory]
    [MemberData(nameof(Guests))]
    public void Test_Create_GuestModel(GuestModel guest)
    {
      var validationContext = new ValidationContext(guest);
      var actual = Validator.TryValidateObject(guest, validationContext, null, true);

      var sut = guest;

      Assert.NotEmpty(guest.FirstName);
      Assert.IsType<string>(guest.FirstName);
      Assert.NotEmpty(guest.LastName);
      Assert.IsType<string>(guest.LastName);
      Assert.IsType<bool>(guest.IsMinor);

      Assert.True(actual);
    }

    [Theory]
    [MemberData(nameof(Guests))]
    public void Test_Validate_GuestModel(GuestModel guest)
    {
      var validationContext = new ValidationContext(guest);
      Assert.Empty(guest.Validate(validationContext));
    }
  }
}
