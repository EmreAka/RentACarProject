using System.Collections.Generic;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrete;

namespace Business.Concrete
{
    public class FuelManager : IFuelService
    {
        private readonly IFuelDal _fuelDal;

        public FuelManager(IFuelDal fuelDal)
        {
            _fuelDal = fuelDal;
        }
        
        public IResult Add(Fuel fuel)
        {
            _fuelDal.Add(fuel);
            return new SuccessResult("Successfully Added");
        }

        public IResult Delete(Fuel fuel)
        {
            _fuelDal.Delete(fuel);
            return new SuccessResult("Successfully Deleted");
        }

        public IResult Update(Fuel fuel)
        {
            _fuelDal.Update(fuel);
            return new SuccessResult("Successfully Updated");
        }

        public IDataResult<List<Fuel>> GetAll()
        {
            var result = _fuelDal.GetAll();
            return new SuccessDataResult<List<Fuel>>(result);
        }

        public IDataResult<Fuel> GetById(int id)
        {
            var result = _fuelDal.Get(f => f.Id == id);
            return new SuccessDataResult<Fuel>(result);
        }
    }
}