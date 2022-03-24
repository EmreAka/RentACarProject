using System.Collections.Generic;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrete;

namespace Business.Concrete
{
    public class EngineManager : IEngineService
    {
        private readonly IEngineDal _engineDal;

        public EngineManager(IEngineDal engineDal)
        {
            _engineDal = engineDal;
        }
        
        [SecuredOperation("admin")]
        [CacheRemoveAspect("IEngineService.Get")]
        public IResult Add(Engine engine)
        {
            _engineDal.Add(engine);
            return new SuccessResult("Successfully Added");
        }
        
        [SecuredOperation("admin")]
        [CacheRemoveAspect("IEngineService.Get")]
        public IResult Delete(Engine engine)
        {
            _engineDal.Delete(engine);
            return new SuccessResult("Successfully Deleted");
        }
        
        [SecuredOperation("admin")]
        [CacheRemoveAspect("IEngineService.Get")]
        public IResult Update(Engine engine)
        {
            _engineDal.Update(engine);
            return new SuccessResult("Successfully Updated");
        }
        
        [CacheAspect()]
        public IDataResult<List<Engine>> GetAll()
        {
            return new SuccessDataResult<List<Engine>>(_engineDal.GetAll());
        }

        [CacheAspect()]
        public IDataResult<Engine> GetById(int id)
        {
            return new SuccessDataResult<Engine>(_engineDal.Get(e => e.Id == id));
        }
    }
}