using System.Collections.Generic;
using Core.DataAccess;
using Entity.Concrete;
using Entity.DTOs;

namespace DataAccess.Abstract
{
    public interface IFavoriteDal: IEntityRepository<Favorite>
    {
        List<FavoriteDetailDto> GetFavoriteDetailsByUserId(int userId);
    }
}