using System.Collections.Generic;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.DTOs;

namespace Business.Concrete
{
    public class FavoriteManager: IFavoriteService
    {
        private readonly IFavoriteDal _favoriteDal;

        public FavoriteManager(IFavoriteDal favoriteDal)
        {
            _favoriteDal = favoriteDal;
        }
        
        [CacheAspect()]
        public IDataResult<List<Favorite>> GetAll()
        {
            return new SuccessDataResult<List<Favorite>>(_favoriteDal.GetAll());
        }
        
        [CacheAspect()]
        public IDataResult<Favorite> GetById(int id)
        {
            return new SuccessDataResult<Favorite>(_favoriteDal.Get(f => f.Id == id));
        }
        
        [CacheAspect()]
        public IDataResult<List<Favorite>> GetFavoritesByUserId(int userId)
        {
            return new SuccessDataResult<List<Favorite>>(_favoriteDal.GetAll(f => f.UserId == userId));
        }
        
        [CacheAspect()]
        public IDataResult<List<FavoriteDetailDto>> GetFavoriteDetailsByUserId(int userId)
        {
            return new SuccessDataResult<List<FavoriteDetailDto>>(_favoriteDal.GetFavoriteDetailsByUserId(userId));
        }

        [CacheRemoveAspect("IFavoriteService.Get")]
        [SecuredOperation]
        [PerformanceAspect(2)]
        public IResult Add(Favorite favorite)
        {
            _favoriteDal.Add(favorite);
            return new SuccessResult();
        }
        
        [CacheRemoveAspect("IFavoriteService.Get")]
        [SecuredOperation]
        public IResult Delete(Favorite favorite)
        {
            _favoriteDal.Delete(favorite);
            return new SuccessResult();
        }
        
        [CacheRemoveAspect("IFavoriteService.Get")]
        [SecuredOperation]
        public IResult Update(Favorite favorite)
        {
            _favoriteDal.Update(favorite);
            return new SuccessResult();
        }
    }
}