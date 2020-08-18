using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RVTR.Booking.ObjectModel.Models;
using Xunit;

namespace RVTR.Booking.UnitTesting.Tests
{
  public class RentalModelTest
  {
    public static readonly IEnumerable<Object[]> _rentals = new List<Object[]>
        {
            new object[]
            {
                new RentalModel()
                {
                    Id = 0,
                    BookingId = 0,
                    Booking = null
                }
            }
        };

    [Theory]
    [MemberData(nameof(_rentals))]
    public void Test_Create_RentalModel(RentalModel rental)
    {
      var validationContext = new ValidationContext(rental);
      var actual = Validator.TryValidateObject(rental, validationContext, null, true);

      Assert.True(actual);
    }

    [Theory]
    [MemberData(nameof(_rentals))]
    public void Test_Validate_RentalModel(RentalModel rental)
    {
      var validationContext = new ValidationContext(rental);
      Assert.Empty(rental.Validate(validationContext));
    }
  }
}
