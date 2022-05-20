﻿using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using Business.BusinessAspects.Autofac;
using Microsoft.AspNetCore.Http;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.CloudinaryAdapter;
using Core.Utilities.Business;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        private readonly ICarImageDal _carImageDal;
        
        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        [CacheRemoveAspect("ICarImageService.Get")]
        [CacheRemoveAspect("ICarService.Get")]
        [SecuredAddImageOperation]
        public IResult Add(CarImage carImage, IFormFile file)
        {
            var result = BusinessRules.Run(CheckIfImageLimitExceded(carImage.CarId));
            if (result != null)
            {
                return result;
            }

            var respond = CloudinaryAdapter.UploadPhoto(file);
            carImage.Date = DateTime.Now;
            carImage.ImageUrl = respond;
            _carImageDal.Add(carImage);
            return new SuccessResult();
        }
        
        [CacheRemoveAspect("ICarService.Get")]
        [SecuredOperation()]
        public IResult AddRange(int carId, List<IFormFile> file)
        {
            List<CarImage> carImages = new List<CarImage>();
            foreach (IFormFile x in file)
            {
                var respond = CloudinaryAdapter.UploadPhoto(x);
                carImages.Add(new CarImage()
                {
                    Date = DateTime.UtcNow,
                    CarId = carId,
                    ImageUrl = respond
                });
            }
            _carImageDal.AddRange(carImages);
            return new SuccessResult();
        }

        [CacheRemoveAspect("ICarImageService.Get")]
        [CacheRemoveAspect("ICarService.Get")]
        [SecuredUpdateImageOperation]
        public IResult Delete(CarImage carImage)
        {
            _carImageDal.Delete(carImage);
            return new SuccessResult("Car Image deleted successfully");
        }

        [CacheAspect]
        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        [CacheAspect]
        public IDataResult<List<CarImage>> GetAllByCarId(int carId)
        {
            var result = BusinessRules.Run(CheckIfCarHasAnyImage(carId));
            if (result != null)
            {
                return new ErrorDataResult<List<CarImage>>(new List<CarImage>()
                    {
                        new CarImage()
                        {
                            CarId = carId,
                            ImageUrl = "https://res.cloudinary.com/emreaka/image/upload/v1624304366/job_o67inx.jpg"
                        }
                    },
                    "This car has no image");
            }

            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(i => i.CarId == carId));
        }

        [CacheAspect]
        public IDataResult<CarImage> GetById(int id)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(i => i.Id == id));
        }

        [CacheRemoveAspect("ICarImageService.Get")]
        [CacheRemoveAspect("ICarService.Get")]
        [SecuredUpdateImageOperation]
        public IResult Update(CarImage carImage)
        {
            var result = BusinessRules.Run(CheckIfImageLimitExceded(carImage.CarId));
            if (result != null)
            {
                return result;
            }

            _carImageDal.Update(carImage);
            return new SuccessResult();
        }

        [CacheRemoveAspect("ICarImageService.Get")]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult DeleteById(int id)
        {
            var image = _carImageDal.Get(i => i.Id == id);
            if (image == null)
            {
                return new ErrorResult("There is no Image with this id.");
            }

            _carImageDal.Delete(image);
            return new SuccessResult("Car Image deleted successfully");
        }

        private IResult CheckIfImageLimitExceded(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId).Count;
            if (result > 5)
            {
                return new ErrorResult("You can upload max 5 image.");
            }

            return new SuccessResult();
        }

        private IResult CheckIfCarHasAnyImage(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId).Any();
            if (!result)
            {
                return new ErrorResult();
            }

            return new SuccessResult();
        }
    }
}