using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RVTR.Booking.Domain.Models;
using Xunit;

namespace RVTR.Booking.Testing.Tests
{
  public class BookingModelTest
  {
    public static readonly IEnumerable<object[]> Bookings = new List<object[]>
    {
      new object[]
      {
        //This test has to be modified
        // This model will need to be updated on January 1st, 2021, becasue the BookingModel class checks
      // to make sure that the check in date is not earlier than "today's date"
        new BookingModel()
        {
          EntityId = 0,
          AccountId = 0,
          LodgingId = 0,
          Guests = new List<GuestModel>(),
          Rentals = new List<RentalModel>(),
          CheckIn = new DateTime(2021, 12, 1),
          CheckOut = new DateTime(2021, 12, 2)
        }
      }
    };

    [Theory]
    [MemberData(nameof(Bookings))]
    public void Test_Create_BookingModel(BookingModel booking)
    {
      var validationContext = new ValidationContext(booking);
      var actual = Validator.TryValidateObject(booking, validationContext, null, true);

      Assert.True(actual);
    }

    [Fact]
    public void Test_Create_BookingModel_Fail()
    {
      BookingModel booking = new BookingModel()
      {
        EntityId = 0,
        AccountId = 0,
        LodgingId = 0,
        Guests = null,
        Rentals = null
      };
      var validationContext = new ValidationContext(booking);
      var actual = Validator.TryValidateObject(booking, validationContext, null, true);

      Assert.False(actual);
    }

    [Theory]
    [MemberData(nameof(Bookings))]
    public void Test_Validate_BookingModel(BookingModel booking)
    {
      var validationContext = new ValidationContext(booking);
      Assert.Empty(booking.Validate(validationContext));
    }

    [Fact]
    public void Test_Validate_BookingModel_Fail()
    {
      //This test has to be modified
      // This test method will need to be updated on January 1st, 2021, becasue the BookingModel class checks
      // to make sure that the check in date is not earlier than "today's date"
      BookingModel booking = new BookingModel()
      {
        EntityId = 0,
        AccountId = 0,
        LodgingId = 0,
        Guests = null,
        Rentals = null,
        CheckIn = new DateTime(2020, 12, 1),
        CheckOut = new DateTime(2021, 12, 2)
      };
      var validationContext = new ValidationContext(booking);
      Assert.NotEmpty(booking.Validate(validationContext));

      booking.Guests = new List<GuestModel>();
      validationContext = new ValidationContext(booking);
      Assert.NotEmpty(booking.Validate(validationContext));

      booking.Rentals = new List<RentalModel>();
      validationContext = new ValidationContext(booking);
      Assert.NotEmpty(booking.Validate(validationContext));

      booking.CheckIn = new System.DateTime(2021, 12, 2);
      validationContext = new ValidationContext(booking);
      Assert.NotEmpty(booking.Validate(validationContext));
    }
  }
}
