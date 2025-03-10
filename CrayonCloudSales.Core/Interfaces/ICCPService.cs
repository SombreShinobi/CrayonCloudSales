using CrayonCloudSales.Core.Models;

namespace CrayonCloudSales.Core.Interfaces
{
    public interface ICCPService
    {
        Task<IEnumerable<SoftwareService>> GetAvailableServicesAsync();
        Task<SoftwareService> GetServiceByIdAsync(int id);
        Task<(bool Success, string SubscriptionId)> OrderSoftwareAsync(int id, int quantity, DateTime validToDate);
        Task<bool> UpdateSoftwareQuantityAsync(string subscriptionId, int newQuantity);
        Task<bool> CancelSoftwareAsync(string subscriptionId);
        Task<bool> ActivateSoftwareAsync(string subscriptionId);
        Task<bool> ExtendSoftwareLicenseAsync(string subscriptionId, DateTime newValidToDate);
    }
}
