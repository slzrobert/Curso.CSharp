using Curso.CSharp.Api.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Curso.CSharp.Api.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
