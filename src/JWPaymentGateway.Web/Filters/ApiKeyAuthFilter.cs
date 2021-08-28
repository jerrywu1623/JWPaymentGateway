using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace JWPaymentGateway.Web.Filters
{
    public class ApiKeyAuthFilter: Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("X-API-KEY",
                out var potentialApiKey))
            {
                throw new UnauthorizedAccessException();
            }
            
            await next();
        }
    }
}