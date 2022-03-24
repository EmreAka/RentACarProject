using System.Collections.Generic;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Core.Aspects.Autofac.Caching;
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
        
        [SecuredOperation("admin")]
        [CacheRemoveAspect("IFuelService.Get")]
        public IResult Add(Fuel fuel)
        {
            _fuelDal.Add(fuel);
            return new SuccessResult("Successfully Added");
        }
        
        [SecuredOperation("admin")]
        [CacheRemoveAspect("IFuelService.Get")]
        public IResult Delete(Fuel fuel)
        {
            _fuelDal.Delete(fuel);
            return new SuccessResult("Successfully Deleted");
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("IFuelService.Get")]
        public IResult Update(Fuel fuel)
        {
            _fuelDal.Update(fuel);
            return new SuccessResult("Successfully Updated");
        }
        
        [CacheAspect()]
        public IDataResult<List<Fuel>> GetAll()
        {
            var result = _fuelDal.GetAll();
            return new SuccessDataResult<List<Fuel>>(result);
        }
        
        [CacheAspect()]
        public IDataResult<Fuel> GetById(int id)
        {
            var result = _fuelDal.Get(f => f.Id == id);
            return new SuccessDataResult<Fuel>(result);
        }
    }
}