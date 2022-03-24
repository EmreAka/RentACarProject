using System.Collections.Generic;
using Business.Abstract;
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

        public IResult Add(Engine engine)
        {
            _engineDal.Add(engine);
            return new SuccessResult("Successfully Added");
        }

        public IResult Delete(Engine engine)
        {
            _engineDal.Delete(engine);
            return new SuccessResult("Successfully Deleted");
        }

        public IResult Update(Engine engine)
        {
            _engineDal.Update(engine);
            return new SuccessResult("Successfully Updated");
        }

        public IDataResult<List<Engine>> GetAll()
        {
            return new SuccessDataResult<List<Engine>>(_engineDal.GetAll());
        }

        public IDataResult<Engine> GetById(int id)
        {
            return new SuccessDataResult<Engine>(_engineDal.Get(e => e.Id == id));
        }
    }
}