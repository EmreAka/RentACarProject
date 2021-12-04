using System.Collections.Generic;
using Core.Utilities.Results;
using Entity.Concrete;

namespace Business.Abstract
{
    public interface ICardService
    {
        IDataResult<List<Card>> GetAll();
        IDataResult<List<Card>> GetAllByUserId(int userId);
        IResult Add(Card card);
        IResult Delete(Card card);
        IResult Update(Card card);
    }
}