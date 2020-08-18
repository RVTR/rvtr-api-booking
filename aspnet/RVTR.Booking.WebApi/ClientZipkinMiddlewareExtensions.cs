using Microsoft.AspNetCore.Builder;

namespace RVTR.Booking.WebApi
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
        public static IApplicationBuilder UseZipkin (this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<ClientZipkinMiddleware> ();
        }
    }
}
