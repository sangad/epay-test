using CustomerManagement.Interfaces;
using CustomerManagement.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagement.BusinessLayer
{
    public class CustomerManager : ICustomerManager
    {
        private IMemoryCache _cache;
        private const string customerListCacheKey = "customerList";
        private MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions()
                      .SetSlidingExpiration(TimeSpan.FromSeconds(120000))
                      .SetAbsoluteExpiration(TimeSpan.FromSeconds(240000))
                      .SetPriority(CacheItemPriority.NeverRemove)
                      .SetSize(1000000);
        private List<CustomerModel> _customers = new List<CustomerModel>
        {
           new CustomerModel{ LastName= "Aaaa", FirstName= "Aaaa", Age= 20, ID= 3 },
            new CustomerModel{ LastName= "Aaaa", FirstName= "Bbbb", Age= 56, ID= 2 },
            new CustomerModel{ LastName= "Cccc", FirstName= "Aaaa", Age= 32, ID= 5 },
            new CustomerModel{ LastName= "Cccc", FirstName= "Bbbb", Age= 50, ID= 1 },
            new CustomerModel{ LastName= "Dddd", FirstName= "Aaaa", Age= 70, ID= 4 }
        };
        public CustomerManager(IMemoryCache cache)
        {
            _cache = cache;
        }
        public List<CustomerModel> GetCustomers()
        {
            List<CustomerModel> customers;
            if (!_cache.TryGetValue(customerListCacheKey, out customers))
            {
                _cache.Set(customerListCacheKey, _customers, cacheEntryOptions);
            }
            _cache.TryGetValue(customerListCacheKey, out customers);
            return customers;
        }

        public string SaveCustomers(List<CustomerModel> lstCustomers)
        {
            List<CustomerModel> customers;
            _cache.TryGetValue(customerListCacheKey, out customers);
            if (lstCustomers.Where(a => _customers.Any(b => a.ID == b.ID)).Count() > 0)
                return "Record ID already exists!";

            customers.AddRange(lstCustomers);
            customers = customers.OrderBy(c => c.LastName).ThenBy(c => c.FirstName).ToList();
            _cache.Set(customerListCacheKey, customers, cacheEntryOptions);
            return string.Empty;
        }
    }
}
