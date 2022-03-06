using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal :
        EfEntityRepositoryBase<Car, ReCapProjectContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetailByCarId(int carId)
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from c in context.Cars
                    join b in context.Brands
                        on c.BrandId equals b.Id
                    join co in context.Colours
                        on c.ColourId equals co.Id
                    join u in context.Customers
                        on c.UserId equals u.Id
                    where c.Id == carId
                    select new CarDetailDto
                    {
                        Id = c.Id,
                        BrandName = b.Name,
                        ColourName = co.Name,
                        CompanyName = u.CompanyName,
                        DailyPrice = c.DailyPrice,
                        Description = c.Description,
                        ModelYear = c.ModelYear,
                        Images = (from i in context.CarImages where i.CarId == c.Id select i.ImageUrl).ToList()
                    };
                return result.ToList();
            }
        }

        public List<CarDetailDto> GetCarDetailsByBrandIdAndColourId(int brandId, int colourId)
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from c in context.Cars
                    join b in context.Brands
                        on c.BrandId equals b.Id
                    join co in context.Colours
                        on c.ColourId equals co.Id
                    join u in context.Customers
                        on c.UserId equals u.Id    
                    where c.BrandId == brandId
                    where c.ColourId == colourId
                    select new CarDetailDto
                    {
                        Id = c.Id,
                        BrandName = b.Name,
                        ColourName = co.Name,
                        CompanyName = u.CompanyName,
                        DailyPrice = c.DailyPrice,
                        Description = c.Description,
                        ModelYear = c.ModelYear,
                        Images = (from i in context.CarImages where i.CarId == c.Id select i.ImageUrl).ToList()
                    };
                return result.ToList();
            }
        }

        public List<CarDetailDto> GetCarDetails()
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from c in context.Cars
                    join b in context.Brands
                        on c.BrandId equals b.Id
                    join co in context.Colours
                        on c.ColourId equals co.Id
                    join u in context.Customers
                        on c.UserId equals u.Id
                    select new CarDetailDto
                    {
                        Id = c.Id,
                        BrandName = b.Name,
                        ColourName = co.Name,
                        CompanyName = u.CompanyName,
                        DailyPrice = c.DailyPrice,
                        Description = c.Description,
                        ModelYear = c.ModelYear,
                        Images = (from i in context.CarImages where i.CarId == c.Id select i.ImageUrl).ToList()
                    };
                return result.ToList();
            }
        }

        public List<CarDetailDto> GetCarDetailsByBrandId(int brandId)
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from c in context.Cars
                    join b in context.Brands
                        on c.BrandId equals b.Id
                    join co in context.Colours
                        on c.ColourId equals co.Id
                    join u in context.Customers
                        on c.UserId equals u.Id
                    where c.BrandId == brandId
                    select new CarDetailDto
                    {
                        Id = c.Id,
                        BrandName = b.Name,
                        ColourName = co.Name,
                        CompanyName = u.CompanyName,
                        DailyPrice = c.DailyPrice,
                        Description = c.Description,
                        ModelYear = c.ModelYear,
                        Images = (from i in context.CarImages where i.CarId == c.Id select i.ImageUrl).ToList()
                    };
                return result.ToList();
            }
        }

        public List<CarDetailDto> GetCarDetailsByColourId(int colourId)
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from c in context.Cars
                    join b in context.Brands
                        on c.BrandId equals b.Id
                    join co in context.Colours
                        on c.ColourId equals co.Id
                    join u in context.Customers
                        on c.UserId equals u.Id
                    where c.ColourId == colourId
                    select new CarDetailDto
                    {
                        Id = c.Id,
                        BrandName = b.Name,
                        ColourName = co.Name,
                        CompanyName = u.CompanyName,
                        DailyPrice = c.DailyPrice,
                        Description = c.Description,
                        ModelYear = c.ModelYear,
                        Images = (from i in context.CarImages where i.CarId == c.Id select i.ImageUrl).ToList()
                    };
                return result.ToList();
            }
        }

        public List<CarDetailDto> GetCarDetailsByCustomerId(int customerId)
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from c in context.Cars
                    join b in context.Brands
                        on c.BrandId equals b.Id
                    join co in context.Colours
                        on c.ColourId equals co.Id
                    join u in context.Customers
                        on c.UserId equals u.Id
                    where c.UserId == customerId
                    select new CarDetailDto
                    {
                        Id = c.Id,
                        BrandName = b.Name,
                        ColourName = co.Name,
                        CompanyName = u.CompanyName,
                        DailyPrice = c.DailyPrice,
                        Description = c.Description,
                        ModelYear = c.ModelYear,
                        Images = (from i in context.CarImages where i.CarId == c.Id select i.ImageUrl).ToList()
                    };
                return result.ToList();
            }
        }
    }
}