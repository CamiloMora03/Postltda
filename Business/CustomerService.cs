using System;
using System.Collections.Generic;
using System.Text;
using Business;
using DataAccess;
using DataAccess.Data;

namespace Business
{
    public class CustomerService : BaseService<Customer>
    {
        private CustomerModel _customerModel;

        public CustomerService(CustomerModel customerModel) : base(customerModel)
        {
            _customerModel = customerModel;
        }

        public virtual Customer GetByName(string name)
        {
            return _customerModel.FindByName(name);
        }
    }


}
