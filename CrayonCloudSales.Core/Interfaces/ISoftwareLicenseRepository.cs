using CrayonCloudSales.Core.Models;

namespace CrayonCloudSales.Core.Interfaces
{
    public interface ISoftwareLicenseRepository
    {
        Task<IEnumerable<SoftwareLicense>> GetLicensesByAccountIdAsync(int accountId);
        Task<SoftwareLicense?> GetLicenseByIdAsync(int licenseId);
        Task<SoftwareLicense> AddLicenseAsync(SoftwareLicense license);
        Task UpdateLicenseAsync(SoftwareLicense license);
        Task<bool> LicenseExists(int accountId, int softwareServiceId);
    }
}
