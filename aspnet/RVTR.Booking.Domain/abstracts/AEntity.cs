namespace RVTR.Booking.Domain
{
   public class AEntity
   {
      /// <summary>
      /// Leverage abstraction to the Id for models. All models need to be inherited from AEntity
      /// </summary>
      public int EntityId { get; set; }
      public AEntity()
      {

      }
   }
}
