using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RVTR.Booking.ObjectModel.Models;
using Xunit;

namespace RVTR.Booking.UnitTesting.Tests
{
  public class RentalModelTest
  {
    public static readonly IEnumerable<object[]> Rentals = new List<object[]>
    {
      new object[]
      {
        new RentalModel()
        {
          Id = 0,
          BookingId = 0,
          Booking = new BookingModel()
        }
      }
    };

    [Theory]
    [MemberData(nameof(Rentals))]
    public void Test_Create_RentalModel(RentalModel rental)
    {
      var validationContext = new ValidationContext(rental);
      var actual = Validator.TryValidateObject(rental, validationContext, null, true);

      Assert.True(actual);
    }

    [Fact]
    public void Test_Create_RentalModel_Fail()
    {
      RentalModel rental = new RentalModel()
      {
        Id = 0,
        BookingId = 0,
        Booking = null
      };
      var validationContext = new ValidationContext(rental);
      var actual = Validator.TryValidateObject(rental, validationContext, null, true);

      Assert.False(actual);
    }

    [Theory]
    [MemberData(nameof(Rentals))]
    public void Test_Validate_RentalModel(RentalModel rental)
    {
      var validationContext = new ValidationContext(rental);
      Assert.Empty(rental.Validate(validationContext));
    }

    [Fact]
    public void Test_Validate_RentalModel_Fail()
    {
      RentalModel rental = new RentalModel()
      {
        Id = 0,
        BookingId = 0,
        Booking = null
      };
      var validationContext = new ValidationContext(rental);
      Assert.NotEmpty(rental.Validate(validationContext));
    }
  }
}
