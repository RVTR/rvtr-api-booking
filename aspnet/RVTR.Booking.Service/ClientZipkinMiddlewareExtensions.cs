using Microsoft.AspNetCore.Builder;

namespace RVTR.Booking.Service
{
  /// <summary>
  ///
  /// </summary>
  internal static class ZipkinClientMiddlewareExtensions
  {
    /// <summary>
    ///
    /// </summary>
    /// <param name="applicationBuilder"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseZipkin(this IApplicationBuilder applicationBuilder)
    {
      return applicationBuilder.UseMiddleware<ClientZipkinMiddleware>();
    }
  }
}
