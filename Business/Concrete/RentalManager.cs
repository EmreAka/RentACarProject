﻿using System.Collections.Generic;
using Business.Abstract;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.DTOs;

namespace Business.Concrete
{
    public class RentalManager: IRentalService
    {
        private IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        [CacheAspect]
        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }

        [CacheAspect]
        public IDataResult<Rental> GetById(int id)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(r => r.Id == id));
        }

        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Add(Rental rental)
        {
            Rental result =  _rentalDal.Get(r => r.CarId == rental.CarId && r.ReturnDate == null);
            if (result != null)
            {
                return new ErrorResult("This car is not available");
            }
            _rentalDal.Add(rental);
            return new SuccessResult("Rental added");
        }

        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult("Rental deleted");
        }

        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Update(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult("Rental updated");
        }

        [CacheAspect]
        public IDataResult<List<RentalDetailDto>> GetDetails()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails());
        }
    }
}