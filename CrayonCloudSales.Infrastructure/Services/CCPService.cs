using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CrayonCloudSales.Core.Interfaces;
using CrayonCloudSales.Core.Models;

namespace CrayonCloudSales.Infrastructure.Services
{

    public class CCPService : ICCPService
    {
        private readonly HttpClient _httpClient;

        private List<SoftwareService> _services;

        public CCPService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _services = new List<SoftwareService>
            {
                new() { Id = 1, Name = "Microsoft Office", Description = "Office productivity suite", PricePerLicense = 15.99m, IsAvailable = true },
                new() { Id = 2, Name = "Adobe Creative Cloud", Description = "Creative design tools", PricePerLicense = 49.99m, IsAvailable = true },
                new() { Id = 3, Name = "Windows Server", Description = "Server operating system", PricePerLicense = 25.99m, IsAvailable = true },
                new() { Id = 4, Name = "SQL Server", Description = "Database server", PricePerLicense = 35.99m, IsAvailable = true },
                new() { Id = 5, Name = "Salesforce CRM", Description = "Customer relationship management", PricePerLicense = 65.00m, IsAvailable = true }
            };
        }

        public async Task<IEnumerable<SoftwareService>> GetAvailableServicesAsync()
        {
            await Task.Delay(300);

            return _services;
        }

        public async Task<SoftwareService> GetServiceByIdAsync(int id)
        {
            await Task.Delay(300);

            return _services.First(s => s.Id == id);
        }

        public async Task<(bool Success, string SubscriptionId)> OrderSoftwareAsync(int id, int quantity, DateTime validToDate)
        {
            await Task.Delay(500);

            string subscriptionId = $"SUB-{Guid.NewGuid().ToString().Substring(0, 8)}";

            return (true, subscriptionId);
        }

        public async Task<bool> UpdateSoftwareQuantityAsync(string subscriptionId, int newQuantity)
        {
            await Task.Delay(300);

            return true;
        }

        public async Task<bool> CancelSoftwareAsync(string subscriptionId)
        {
            await Task.Delay(300);

            return true;
        }

        public async Task<bool> ActivateSoftwareAsync(string subscriptionId)
        {
            await Task.Delay(300);

            return true;
        }

        public async Task<bool> ExtendSoftwareLicenseAsync(string subscriptionId, DateTime newValidToDate)
        {
            await Task.Delay(300);

            return true;
        }
    }
}
