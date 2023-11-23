using DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public class CustomerModel : BaseModel<Customer>
    {
 

        public CustomerModel(syscomTestContext context) : base(context)
        {
        }

        public Customer FindByName(string name)
        {
            return _dbSet.FirstOrDefault(e => e.Name == name);
        }
    }

}
