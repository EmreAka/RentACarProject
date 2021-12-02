using System;
using System.Collections.Generic;
using Core.Utilities.Results;
using Entity.Concrete;
using Entity.DTOs;

namespace Business.Abstract
{
    public interface IRentalService
    {
        IDataResult<List<Rental>> GetAll();
        IDataResult<Rental> GetById(int id);
        IDataResult<List<RentalDetailDto>> GetDetails();
        IResult Add(Rental rental);
        IResult Delete(Rental rental);
        IResult Update(Rental rental);
        IDataResult<List<RentalDetailDto>> GetDetailByCarId(int carId);
        IResult CheckIfCarIsAvailable(int carId, DateTime rentDate, DateTime returnDate);
    }
}