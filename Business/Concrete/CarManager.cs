using System;
using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.DTOs;
using System.Collections.Generic;
using System.Linq;
using Business.BusinessAspects.Autofac;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {

        private readonly ICarDal _carDal;
        private readonly ICarImageService _carImageService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CarManager(ICarDal carDal, ICarImageService carImageService)
        {
            _carDal = carDal;
            _carImageService = carImageService;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        [SecuredOperation("admin")]
        public IResult Add(Car car)
        {
            _carDal.Add(car);
            return new SuccessResult("Car added");
        }

        [CacheRemoveAspect("ICarService.Get")]
        [SecuredUpdateCarOperation]
        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult("Car deleted");
        }

        [CacheAspect]
        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll());
        }

        [CacheAspect]
        [PerformanceAspect(2)]
        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.Id == id));
        }

        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetCarDetailByCarId(int carId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetailByCarId(carId));
        }
        
        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetCarDetailsByBrandIdAndColourId(int brandId, int colourId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetailsByBrandIdAndColourId(brandId, colourId));
        }
        
        [CacheAspect()]
        public IDataResult<List<CarDetailDto>> GetCarDetailsByCustomerId()
        {
            var userId = _httpContextAccessor.HttpContext.User.Identities.ToList()[0].Claims.ToList()[0].Value;
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetailsByCustomerId(Int32.Parse(userId)));
        }

        [ValidationAspect(typeof(CarForAddValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        [SecuredOperation]
        public IResult AddWithImages(CarForAddDto carForAddDto)
        {
            var userId = _httpContextAccessor.HttpContext.User.Identities.ToList()[0].Claims.ToList()[0].Value;
            Car carToAdd = new Car()
            {
                Description = carForAddDto.Description,
                BrandId = carForAddDto.BrandId,
                ColourId = carForAddDto.ColourId,
                DailyPrice = carForAddDto.DailyPrice,
                DoorNumber = carForAddDto.DoorNumber,
                EngineId = carForAddDto.EngineId,
                FuelConsumption = carForAddDto.FuelConsumption,
                FuelId = carForAddDto.FuelId,
                ModelYear = carForAddDto.ModelYear,
                UserId = Int32.Parse(userId)
            };
            
            _carDal.Add(carToAdd);
            int id = carToAdd.Id;

            _carImageService.AddRange(id, carForAddDto.Images);
            
            return new SuccessResult("Success");
        }

        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails());
        }

        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetCarDetailsByBrandId(int brandId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetailsByBrandId(brandId));
        }
        
        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetCarDetailsByColourId(int colourId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetailsByColourId(colourId));
        }

        [CacheAspect]
        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == id));
        }

        [CacheAspect]
        public IDataResult<List<Car>> GetCarsByColourId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColourId == id));
        }

        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        [SecuredUpdateCarOperation]
        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult("Car updated");
        }
    }
}
