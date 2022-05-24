using System;
using System.Collections.Generic;
using System.Linq;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Concrete
{
    public class FavoriteManager: IFavoriteService
    {
        private readonly IFavoriteDal _favoriteDal;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FavoriteManager(IFavoriteDal favoriteDal)
        {
            _favoriteDal = favoriteDal;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
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
        [SecuredOperation]
        public IDataResult<List<Favorite>> GetFavoritesByUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.Identities.ToList()[0].Claims.ToList()[0].Value;
            return new SuccessDataResult<List<Favorite>>(_favoriteDal.GetAll(f => f.UserId == Int32.Parse(userId)));
        }
        
        [CacheAspect()]
        [SecuredOperation]
        public IDataResult<List<FavoriteDetailDto>> GetFavoriteDetailsByUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.Identities.ToList()[0].Claims.ToList()[0].Value;
            return new SuccessDataResult<List<FavoriteDetailDto>>(_favoriteDal.GetFavoriteDetailsByUserId(Int32.Parse(userId)));
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