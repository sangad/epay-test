using CustomerManagement.Interfaces;
using CustomerManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CustomerManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly ICustomerManager _customerManager;
        public CustomerController(ICustomerManager customerManager)
        {
            _customerManager = customerManager;
        }
        // GET: api/<Customer>
        [HttpGet]
        public IEnumerable<CustomerModel> Get()
        {
            var lstCustomer = _customerManager.GetCustomers();
            return lstCustomer;
        }


        // POST api/<Customer>
        [HttpPost]
        public IActionResult Post([FromBody] List<CustomerModel> customers)
        {
            var result = _customerManager.SaveCustomers(customers);
            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);
            return Ok(System.Net.HttpStatusCode.Created);
        }

    }
}
