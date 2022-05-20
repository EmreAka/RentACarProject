using System.Collections.Generic;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.DTOs;

namespace Business.Concrete
{
    public class CustomerManager: ICustomerService
    {
        private readonly ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        [CacheAspect]
        public IDataResult<List<Customer>> GetAll()
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll());
        }

        [CacheAspect]
        public IDataResult<List<CustomerDetailDto>> GetCustomerDetails()
        {
            return new SuccessDataResult<List<CustomerDetailDto>>(_customerDal.GetCustomerDetails());
        }

        [CacheAspect]
        public IDataResult<Customer> GetById(int id)
        {
            return new SuccessDataResult<Customer>(_customerDal.Get(c => c.Id == id));
        }

        [CacheRemoveAspect("ICustomerService.Get")]
        public IResult Add(Customer customer)
        {
            _customerDal.Add(customer);
            return new SuccessResult("Customer added");
        }

        [CacheRemoveAspect("ICustomerService.Get")]
        [SecuredOperation("admin")]
        public IResult Delete(Customer customer)
        {
            _customerDal.Delete(customer);
            return new SuccessResult("Customer deleted");
        }

        [CacheRemoveAspect("ICustomerService.Get")]
        [SecuredOperation]
        public IResult Update(Customer customer)
        {
            _customerDal.Update(customer);
            return new SuccessResult("Customer updated");
        }

        public IDataResult<List<CustomerDetailDto>> GetCustomerDetailByEmail(string email)
        {
            return new SuccessDataResult<List<CustomerDetailDto>>(_customerDal.GetCustomerDetailByEmail(email));
        }
        
        [CacheRemoveAspect("ICustomerService.Get")]
        [SecuredOperation]
        public IResult UpdateDetails(CustomerDetailDto customerDetailDto)
        {
            var customerToUpdate = _customerDal.Get(c => c.Email == customerDetailDto.Email);
            customerToUpdate.FirstName = customerDetailDto.FirstName;
            customerToUpdate.LastName = customerDetailDto.LastName;
            customerToUpdate.Email = customerDetailDto.Email;
            customerToUpdate.CompanyName = customerDetailDto.CompanyName;
            _customerDal.Update(customerToUpdate);
            return new SuccessResult("Customer details updated");
        }
    }
}