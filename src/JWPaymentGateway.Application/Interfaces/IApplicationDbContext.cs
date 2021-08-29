using System.Threading;
using System.Threading.Tasks;
using JWPaymentGateway.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JWPaymentGateway.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Merchant> Merchants { get; set; }
        
        DbSet<MerchantKey> MerchantKeys { get; set; }
        
        DbSet<Card> Cards { get; set; }
        
        DbSet<Transaction> Transactions { get; set; }
        
        DbSet<Payment> Payments { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}