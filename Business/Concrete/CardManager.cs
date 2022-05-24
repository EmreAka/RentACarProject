using System;
using System.Collections.Generic;
using System.Linq;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Concrete
{
    public class CardManager : ICardService
    {
        private readonly ICardDal _cardDal;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CardManager(ICardDal cardDal)
        {
            _cardDal = cardDal;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }
        
        [CacheAspect()]
        public IDataResult<List<Card>> GetAll()
        {
            return new SuccessDataResult<List<Card>>(_cardDal.GetAll());
        }
        
        [CacheAspect()]
        [SecuredOperation]
        public IDataResult<List<Card>> GetAllByUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.Identities.ToList()[0].Claims.ToList()[0].Value;
            return new SuccessDataResult<List<Card>>(_cardDal.GetAll(c => c.UserId == Int32.Parse(userId)));
        }
        
        [SecuredOperation()]
        [CacheRemoveAspect("ICardService.Get")]
        public IResult Add(Card card)
        {
            _cardDal.Add(card);
            return new SuccessResult();
        }
        
        [SecuredOperation("admin")]
        [CacheRemoveAspect("ICardService.Get")]
        public IResult Delete(Card card)
        {
            _cardDal.Delete(card);
            return new SuccessResult();
        }
        
        [SecuredOperation("admin")]
        [CacheRemoveAspect("ICardService.Get")]
        public IResult Update(Card card)
        {
            _cardDal.Update(card);
            return new SuccessResult();
        }
    }
}