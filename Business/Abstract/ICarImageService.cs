﻿using Core.Utilities.Results;
using Entity.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        IDataResult<List<CarImage>> GetAll();
        IDataResult<CarImage> GetById(int id);
        IDataResult<List<CarImage>> GetAllByCarId(int carId);
        IResult Add(CarImage carImage, IFormFile file);
        IResult AddRange(int carId, List<IFormFile> file);
        IResult Delete(CarImage carImage);
        IResult Update(CarImage carImage);
        IResult DeleteById(int id);
    }
}
