using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System.IO;
using Core.Utilities.CloudinaryAdapter;
using Core.Utilities.Business;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;


        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        public IResult Add(CarImage carImage, IFormFile file)
        {
            var result = BusinessRules.Run(CheckIfImageLimitExceded());
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

        public IResult Delete(CarImage carImage)
        {
            _carImageDal.Delete(carImage);
            return new SuccessResult();
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        public IDataResult<List<CarImage>> GetAllByCarId(int carId)
        {
            var result = BusinessRules.Run(CheckIfCarHasAnyImage(carId));
            if (result != null)
            {
                return new ErrorDataResult<List<CarImage>>(new List<CarImage>() { new CarImage() {
                CarId = carId, ImageUrl = "https://res.cloudinary.com/emreaka/image/upload/v1624304366/job_o67inx.jpg"} },
                "This car has no image");
            }
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(i => i.CarId == carId));
        }

        public IDataResult<CarImage> GetById(int id)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(i => i.Id == id));
        }

        public IResult Update(CarImage carImage)
        {
            var result = BusinessRules.Run(CheckIfImageLimitExceded());
            if (result != null)
            {
                return result;
            }
            _carImageDal.Update(carImage);
            return new SuccessResult();
        }

        private IResult CheckIfImageLimitExceded()
        {
            var result = _carImageDal.GetAll().Count;
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
