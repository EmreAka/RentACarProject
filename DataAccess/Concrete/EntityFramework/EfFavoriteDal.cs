using System;
using System.Collections.Generic;
using System.Linq;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfFavoriteDal : EfEntityRepositoryBase<Favorite, ReCapProjectContext>, IFavoriteDal
    {
        public List<FavoriteDetailDto> GetFavoriteDetailsByUserId(int userId)
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from f in context.Favorites
                    join c in context.Cars
                        on f.CarId equals c.Id
                    join b in context.Brands
                        on c.BrandId equals b.Id
                    join co in context.Colours
                        on c.ColourId equals co.Id
                    join u in context.Customers
                        on f.UserId equals u.Id
                    where f.UserId == userId
                    select new FavoriteDetailDto
                    {
                        Id = f.Id,
                        CarId = f.CarId,
                        BrandName = b.Name,
                        ColourName = co.Name,
                        CompanyName = u.CompanyName,
                        Description = c.Description,
                        DailyPrice = c.DailyPrice,
                        ModelYear = c.ModelYear,
                        Images = (from i in context.CarImages where i.CarId == c.Id select i.ImageUrl).ToList()
                    };
                return result.ToList();
            }
        }
    }
}