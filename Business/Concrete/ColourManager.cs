using Business.Abstract;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrete;
using System.Collections.Generic;
using Business.BusinessAspects.Autofac;

namespace Business.Concrete
{
    public class ColourManager : IColourService
    {
        private readonly IColourDal _colourDal;

        public ColourManager(IColourDal colourDal)
        {
            _colourDal = colourDal;
        }

        [CacheRemoveAspect("IColourService.Get")]
        [SecuredOperation("admin")]
        public IResult Add(Colour colour)
        {
            _colourDal.Add(colour);
            return new SuccessResult("Colour added");
        }

        [CacheRemoveAspect("IColourService.Get")]
        [SecuredOperation("admin")]
        public IResult Delete(Colour colour)
        {
            _colourDal.Delete(colour);
            return new SuccessResult("Colour deleted");
        }

        [CacheAspect]
        public IDataResult<List<Colour>> GetAll()
        {
            return new SuccessDataResult<List<Colour>>(_colourDal.GetAll());
        }

        [CacheAspect]
        public IDataResult<Colour> GetById(int id)
        {
            return new SuccessDataResult<Colour>(_colourDal.Get(c => c.Id == id));
        }

        [CacheRemoveAspect("IColourService.Get")]
        [SecuredOperation("admin")]
        public IResult Update(Colour colour)
        {
            _colourDal.Update(colour);
            return new SuccessResult("Colour updated");
        }
    }
}
