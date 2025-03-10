using Microsoft.EntityFrameworkCore;
using CrayonCloudSales.Core.Interfaces;
using CrayonCloudSales.Core.Models;
using CrayonCloudSales.Infrastructure.Data;

namespace CrayonCloudSales.Infrastructure.Repositories
{
    
    public class SoftwareLicenseRepository : ISoftwareLicenseRepository
    {
        private readonly CloudSalesDbContext _dbContext;
        
        public SoftwareLicenseRepository(CloudSalesDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<IEnumerable<SoftwareLicense>> GetLicensesByAccountIdAsync(int accountId)
        {
            return await _dbContext.SoftwareLicenses
                .Where(l => l.AccountId == accountId)
                .ToListAsync();
        }
        
        public async Task<SoftwareLicense?> GetLicenseByIdAsync(int licenseId)
        {
            return await _dbContext.SoftwareLicenses
                .FirstOrDefaultAsync(l => l.Id == licenseId);
        }
        
        public async Task<SoftwareLicense> AddLicenseAsync(SoftwareLicense license)
        {
            _dbContext.SoftwareLicenses.Add(license);
            await _dbContext.SaveChangesAsync();
            return license;
        }
        
        public async Task UpdateLicenseAsync(SoftwareLicense license)
        {
            _dbContext.Entry(license).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> LicenseExists(int accountId, int softwareServiceId)
        {
            var license = await _dbContext.SoftwareLicenses.FirstOrDefaultAsync(sl => sl.AccountId == accountId && sl.SoftwareServiceId == softwareServiceId);
            if (license == null)
            {
                return false;
            }

            return true;
        }
    }
}
