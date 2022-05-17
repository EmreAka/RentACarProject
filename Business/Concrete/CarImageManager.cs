using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using Business.BusinessAspects.Autofac;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Core.Aspects.Autofac.Caching;
using Core.Exceptions;
using Core.Utilities.CloudinaryAdapter;
using Core.Utilities.Business;
using Core.Utilities.IoC;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;


        public CarImageManager(ICarImageDal carImageDal, ICarService carService)
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

        [CacheRemoveAspect("ICarImageService.Get")]
        [CacheRemoveAspect("ICarService.Get")]
        [SecuredUpdateImageOperation]
        public IResult Delete(CarImage carImage)
        {
            // var result = BusinessRules.Run(CheckIfUserIsAllowedToDeleteThisImage(carImage.CarId));
            // if (result != null)
            // {
            //     throw new AuthorizationDeniedException("Authorization Denied");
            // }

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

        /*private IResult CheckIfUserIsAllowedToDeleteThisImage(int carId)
        {
            var claimsIdentities = _httpContextAccessor.HttpContext.User.Identities;
            var result = _carService.GetById(carId);
            if (result != null)
            {
                foreach (var claimIdentity in claimsIdentities)
                {
                    var claim = claimIdentity.Claims.ToList();

                    if (result.Data.UserId == Int32.Parse(claim[0].Value))
                    {
                        return new SuccessResult();
                    }
                }
            }

            return new ErrorResult();
        }*/
    }
}