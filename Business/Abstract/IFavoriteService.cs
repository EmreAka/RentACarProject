using System.Collections.Generic;
using Core.Utilities.Results;
using Entity.Concrete;
using Entity.DTOs;

namespace Business.Abstract
{
    public interface IFavoriteService
    {
        IDataResult<List<Favorite>> GetAll();
        IDataResult<Favorite> GetById(int id);
        IDataResult<List<Favorite>> GetFavoritesByUserId(int userId);
        IDataResult<List<FavoriteDetailDto>> GetFavoriteDetailsByUserId(int userId);
        IResult Add(Favorite favorite);
        IResult Delete(Favorite favorite);
        IResult Update(Favorite favorite);
    }
}