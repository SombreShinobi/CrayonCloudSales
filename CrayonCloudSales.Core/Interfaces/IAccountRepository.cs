using CrayonCloudSales.Core.Models;

namespace CrayonCloudSales.Core.Interfaces
{

    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAccountsByCustomerIdAsync(int customerId);
        Task<Account> GetAccountByIdAsync(int accountId);
        Task<bool> AccountBelongsToCustomerAsync(int accountId, int customerId);
    }

}
