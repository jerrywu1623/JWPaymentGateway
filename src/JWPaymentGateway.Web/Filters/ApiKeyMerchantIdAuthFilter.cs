using System;
using System.Linq;
using System.Threading.Tasks;
using JWPaymentGateway.Application.Constants;
using JWPaymentGateway.Application.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace JWPaymentGateway.Web.Filters
{
    public class ApiKeyMerchantIdAuthFilter: Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(Constants.HttpHeaders.API_KEY_HEADER_NAME, out var potentialApiKey))
            {
                throw new UnauthorizedAccessException();
            }
            
            if (!context.HttpContext.Request.Headers.TryGetValue(Constants.HttpHeaders.MERCHANT_ID_HEADER_NAME, out var potentialMerchantId))
            {
                throw new UnauthorizedAccessException();
            }

            var apiKey = potentialApiKey.ToString();
            var merchantId = int.Parse(potentialMerchantId);
            var applicationDbContext = context.HttpContext.RequestServices.GetRequiredService<IApplicationDbContext>();
            if (!applicationDbContext.MerchantKeys.Any(w => w.ApiKey == apiKey && w.MerchantId == merchantId))
            {
                throw new UnauthorizedAccessException();
            }

            await next();
        }
    }
}