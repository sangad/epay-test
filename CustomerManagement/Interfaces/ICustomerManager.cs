using CustomerManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagement.Interfaces
{
    public interface ICustomerManager
    {
        string SaveCustomers(List<CustomerModel> lstCustomers);
        List<CustomerModel> GetCustomers();

    }
}
