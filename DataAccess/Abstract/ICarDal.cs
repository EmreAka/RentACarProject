using Core.DataAccess;
using Entity.Concrete;
using Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICarDal:IEntityRepository<Car>
    {
        List<CarDetailDto> GetCarDetails();
        List<CarDetailDto> GetCarDetailsByBrandId(int brandId);
        List<CarDetailDto> GetCarDetailsByColourId(int colourId);
        List<CarDetailDto> GetCarDetailByCarId(int carId);
        List<CarDetailDto> GetCarDetailsByBrandIdAndColourId(int brandId, int colourId);
        List<CarDetailDto> GetCarDetailsByCustomerId(int customerId);
        List<CarDetailDto> GetCarDetailsPaginated(int page, string brandName, string colourName);
    }
}
