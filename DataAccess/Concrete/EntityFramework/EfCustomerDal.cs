using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCustomerDal:
        EfEntityRepositoryBase<Customer, ReCapProjectContext>, ICustomerDal
    {
        public List<CustomerDetailDto> GetCustomerDetails()
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from c in context.Customers
                             join u in context.Users
                             on c.Id equals u.Id
                             select new CustomerDetailDto
                             { Id = c.Id, FirstName = c.FirstName, LastName = c.LastName, 
                             CompanyName = c.CompanyName, Status = c.Status};
                return result.ToList();
            } 
        }
    }
}
