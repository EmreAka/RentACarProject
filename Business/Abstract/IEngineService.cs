using System.Collections.Generic;
using Core.Utilities.Results;
using Entity.Concrete;

namespace Business.Abstract
{
    public interface IEngineService
    {
        IResult Add(Engine engine);
        IResult Delete(Engine engine);
        IResult Update(Engine engine);
        IDataResult<List<Engine>> GetAll();
        IDataResult<Engine> GetById(int id);
    }
}