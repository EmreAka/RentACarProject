using System.Collections.Generic;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrete;

namespace Business.Concrete
{
    public class CardManager : ICardService
    {
        private readonly ICardDal _cardDal;

        public CardManager(ICardDal cardDal)
        {
            _cardDal = cardDal;
        }
        
        [CacheAspect()]
        public IDataResult<List<Card>> GetAll()
        {
            return new SuccessDataResult<List<Card>>(_cardDal.GetAll());
        }
        
        [CacheAspect()]
        public IDataResult<List<Card>> GetAllByUserId(int userId)
        {
            return new SuccessDataResult<List<Card>>(_cardDal.GetAll(c => c.UserId == userId));
        }
        
        [SecuredOperation()]
        [CacheRemoveAspect("ICardService.Get")]
        public IResult Add(Card card)
        {
            _cardDal.Add(card);
            return new SuccessResult();
        }
        
        [SecuredOperation()]
        [CacheRemoveAspect("ICardService.Get")]
        public IResult Delete(Card card)
        {
            _cardDal.Delete(card);
            return new SuccessResult();
        }
        
        [SecuredOperation]
        [CacheRemoveAspect("ICardService.Get")]
        public IResult Update(Card card)
        {
            _cardDal.Update(card);
            return new SuccessResult();
        }
    }
}