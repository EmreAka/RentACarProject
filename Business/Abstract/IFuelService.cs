using System.Collections.Generic;
using Core.Utilities.Results;
using Entity.Concrete;

namespace Business.Abstract
{
    public interface IFuelService
    {
        IResult Add(Fuel fuel);
        IResult Delete(Fuel fuel);
        IResult Update(Fuel fuel);
        IDataResult<List<Fuel>> GetAll();
        IDataResult<Fuel> GetById(int id);
    }
}