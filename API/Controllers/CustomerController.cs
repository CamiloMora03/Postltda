using Business;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using CustomerEntity = DataAccess.Data.Customer;

namespace API.Controllers.Customer
{
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private BaseService<CustomerEntity> CustomerService1;
        private CustomerService _customerService;
        public CustomerController(BaseService<CustomerEntity> customerService1, CustomerService customerService)
        {
            CustomerService1 = customerService1;
            _customerService = customerService;
        }


        [HttpGet()]
        public IQueryable<CustomerEntity> GetAll()
        {
            return CustomerService1.GetAll();
        }


        [HttpPost()]
        public CustomerEntity Create([FromBodyAttribute] CustomerEntity entity)
        {
            return CreateCustomer(entity);
        }

        private CustomerEntity CreateCustomer(CustomerEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var existingCustomer = _customerService.GetByName(entity.Name);

            if (existingCustomer != null)
            {
                throw new Exception("Ya existe un cliente con el mismo nombre.");
            }
            return _customerService.Create(entity);
        }



        [HttpPut()]
        public CustomerEntity Update(CustomerEntity entity)
        {
            return CustomerService1.Update(entity.CustomerId, entity, out bool changed);
        }

        [HttpDelete()]
        public CustomerEntity Delete([FromBodyAttribute] CustomerEntity entity)
        {
            return CustomerService1.Delete(entity);
        }
    }
}
