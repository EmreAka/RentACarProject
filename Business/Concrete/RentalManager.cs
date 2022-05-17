using System;
using System.Collections.Generic;
using Business.Abstract;
using Castle.Core.Internal;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.DTOs;

namespace Business.Concrete
{
    public class RentalManager: IRentalService
    {
        private IRentalDal _rentalDal;
        private ICarService _carService;

        public RentalManager(IRentalDal rentalDal, ICarService carService)
        {
            _rentalDal = rentalDal;
            _carService = carService;
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

        public IDataResult<List<RentalDetailDto>> GetDetailByCarId(int carId)
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetDetailByCarId(carId));
        }
        
        [CacheAspect]
        public IDataResult<List<RentalDetailDto>> GetDetails()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails());
        }

        public IResult CheckIfCarIsAvailable(int carId, DateTime rentDate, DateTime returnDate)
        {
            var result = _rentalDal.GetAll(r => r.CarId == carId && r.ReturnDate >= rentDate);
            if (result.IsNullOrEmpty() == false)
            {
                return new ErrorResult("This car is not available. It is going to be available in: " +
                                       result[result.Count - 1].ReturnDate.Value.ToString("yyyy-MM-dd"));
            }

            if (rentDate > returnDate)
            {
                return new ErrorResult("Beginning day must be before than ending day.");
            }

            if ((returnDate - rentDate).Days > 365)
            {
                return new ErrorResult("Your renting exceeds 1 year.");
            }

            if (rentDate < DateTime.Now || returnDate < DateTime.Now)
            {
                return new ErrorResult("You should rent the car for the future, not for the past...");
            }

            return new SuccessResult("This car is available.");
        }
        
    }
}