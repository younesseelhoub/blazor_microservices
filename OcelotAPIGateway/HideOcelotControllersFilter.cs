using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OcelotAPIGateway
{
    internal class HideOcelotControllersFilter : IDocumentFilter
    {
        private static readonly string[] _ignoredPaths = {
            "/configuration",
            "/outputcache/{region}"
        };

        //public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        //{
        //    foreach (var ignorePath in _ignoredPaths)
        //    {
        //        swaggerDoc.Paths.Remove(ignorePath);
        //    }
        //}

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {

            foreach (var ignorePath in _ignoredPaths)
            {
                swaggerDoc.Paths.Remove(ignorePath);
            }
        }
    }
}
