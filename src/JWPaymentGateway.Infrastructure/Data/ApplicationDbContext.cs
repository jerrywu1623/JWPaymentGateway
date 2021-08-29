using System.Threading;
using System.Threading.Tasks;
using JWPaymentGateway.Application.Interfaces;
using JWPaymentGateway.Domain.Common;
using JWPaymentGateway.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JWPaymentGateway.Infrastructure.Data
{
    public class ApplicationDbContext: DbContext, IApplicationDbContext
    {
        private readonly IDateTimeService _dateTimeService;
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeService dateTimeService)
            :base(options)
        {
            _dateTimeService = dateTimeService;
        }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<MerchantKey> MerchantKeys { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Payment> Payments { get; set; }
        
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTimeService.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.Modified = _dateTimeService.Now;
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}