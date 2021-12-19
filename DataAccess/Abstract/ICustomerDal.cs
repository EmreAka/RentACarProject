﻿using Core.DataAccess;
using Entity.Concrete;
using Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICustomerDal: IEntityRepository<Customer>
    {
        List<CustomerDetailDto> GetCustomerDetails();
        List<CustomerDetailDto> GetCustomerDetailByEmail(string email);
    }
}
