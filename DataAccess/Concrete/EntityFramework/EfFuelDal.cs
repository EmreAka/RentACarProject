using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entity.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfFuelDal: EfEntityRepositoryBase<Fuel, ReCapProjectContext>, IFuelDal
    {
        
    }
}