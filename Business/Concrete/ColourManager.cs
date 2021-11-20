using Business.Abstract;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ColourManager : IColourService
    {
        IColourDal _colourDal;

        public ColourManager(IColourDal colourDal)
        {
            _colourDal = colourDal;
        }

        [CacheRemoveAspect("IColourService.Get")]
        public IResult Add(Colour colour)
        {
            _colourDal.Add(colour);
            return new SuccessResult("Colour added");
        }

        [CacheRemoveAspect("IColourService.Get")]
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
        public IResult Update(Colour colour)
        {
            _colourDal.Update(colour);
            return new SuccessResult("Colour updated");
        }
    }
}
