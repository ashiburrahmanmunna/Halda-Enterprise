using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.DataAccess.Middleware
{
    public class CompanyIdMiddleware
    {
        private readonly RequestDelegate _next;

        public CompanyIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Retrieve the CompanyId from the cookie
            if (context.Request.Cookies.TryGetValue("ComId", out var comIdString))
            {
                // Store the CompanyId in the HttpContext.Items
                context.Items["ComId"] = comIdString;
            }
            else
            {
                // If CompanyId is null or invalid, set a default CompanyId (you can modify this as needed)
                context.Items["ComId"] = string.Empty; // Assuming string.Empty represents the default CompanyId
            }

            if (context.Request.Cookies.TryGetValue("UserId", out var userIdString) && Guid.TryParse(userIdString, out var userId))
            {
                // Store the CompanyId in the HttpContext.Items
                context.Items["UserId"] = userIdString;
            }
            else
            {
                // If CompanyId is null or invalid, set a default CompanyId (you can modify this as needed)
                context.Items["UserId"] = string.Empty; // Assuming string.Empty represents the default CompanyId
            }

            await _next(context);
        }
    }
}
