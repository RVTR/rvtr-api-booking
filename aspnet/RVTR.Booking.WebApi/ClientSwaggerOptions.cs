using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RVTR.Booking.WebApi
{
  /// <summary>
  ///
  /// </summary>
  public class ClientSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
  {
    private readonly IApiVersionDescriptionProvider _provider;
    private readonly IConfiguration _configuration;

    /// <summary>
    ///
    /// </summary>
    /// <param name="provider"></param>
    /// <param name="configuration"></param>
    public ClientSwaggerOptions (IApiVersionDescriptionProvider provider, IConfiguration configuration)
    {
      _configuration = configuration;
      _provider = provider;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="options"></param>
    public void Configure (SwaggerGenOptions options)
    {
      var xmlFile = $"{Assembly.GetExecutingAssembly ().GetName ().Name}.xml";
      var xmlPath = Path.Combine (AppContext.BaseDirectory, xmlFile);

      options.IncludeXmlComments (xmlPath);

      foreach (var description in _provider.ApiVersionDescriptions)
      {
        options.SwaggerDoc (description.GroupName, Create (description));
      }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="description"></param>
    /// <returns></returns>
    private OpenApiInfo Create (ApiVersionDescription description)
    {
      var info = new OpenApiInfo ()
      {
        Contact = new OpenApiContact () { Email = _configuration["Contact:Email"], Name = _configuration["Contact:Name"], Url = new Uri (_configuration["Contact:Url"]) },
        Description = "<em>Built with ASP.NET API Versioning, Swagger, Swashbuckle</em>",
        License = new OpenApiLicense () { Name = _configuration["License:Name"], Url = new Uri (_configuration["License:Url"]) },
        Title = _configuration["Title"],
        Version = description.ApiVersion.ToString (),
      };

      if (description.IsDeprecated)
      {
        info.Description += "&nbsp;&Colon;&nbsp;<strong>DEPRECATED</strong>";
      }

      return info;
    }
  }
}
