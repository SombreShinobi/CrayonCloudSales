using Microsoft.EntityFrameworkCore;
using CrayonCloudSales.Core.Interfaces;
using CrayonCloudSales.Core.Models;
using CrayonCloudSales.Infrastructure.Data;

namespace CrayonCloudSales.Infrastructure.Repositories
{
    
    public class AccountRepository : IAccountRepository
    {
        private readonly CloudSalesDbContext _dbContext;
        
        public AccountRepository(CloudSalesDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<IEnumerable<Account>> GetAccountsByCustomerIdAsync(int customerId)
        {
            return await _dbContext.Accounts
                .Where(a => a.CustomerId == customerId && a.IsActive)
                .ToListAsync();
        }
        
        public async Task<Account> GetAccountByIdAsync(int accountId)
        {
            return await _dbContext.Accounts
                .FirstOrDefaultAsync(a => a.Id == accountId);
        }
        
        public async Task<bool> AccountBelongsToCustomerAsync(int accountId, int customerId)
        {
            return await _dbContext.Accounts
                .AnyAsync(a => a.Id == accountId && a.CustomerId == customerId);
        }
    }
}
