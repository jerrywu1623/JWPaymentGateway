using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWPaymentGateway.Domain.Entities;

namespace JWPaymentGateway.Infrastructure.Data
{
    public static class ApplicationDbContextSeed
    {
        public static async Task Initialize(ApplicationDbContext applicationDbContext)
        {
            await applicationDbContext.Database.EnsureCreatedAsync();

            if (applicationDbContext.Merchants.Any())
            {
                return;
            }

            var merchant = new Merchant
            {
                Name = "JWMerchant",
                Address = "London",
                Mobile = "07123456789",
                MerchantKeys = new List<MerchantKey>
                {
                    new MerchantKey
                    {
                        ApiKey = "D7DhYaUBlc"
                    }
                }
            };
            
            applicationDbContext.Merchants.Add(merchant);
            await applicationDbContext.SaveChangesAsync();
        }
    }
}