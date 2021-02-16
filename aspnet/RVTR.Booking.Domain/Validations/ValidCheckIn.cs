using System;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Booking.Domain.CustomValidations
{
  public class ValidCheckIn : ValidationAttribute
  {

    DateTime checkOut;

    public override bool IsValid(object value)
    {

        DateTime inputValue = (DateTime)value;
        DateTime now = DateTime.Now;

        if(inputValue < now)
        {
          return false;
        }

        return true;
    }
  }
}
