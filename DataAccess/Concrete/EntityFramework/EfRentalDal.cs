using Core.DataAccess.EntityFramework;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal :
        EfEntityRepositoryBase<Rental, ReCapProjectContext>, IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetails()
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from rental in context.Rentals
                    join customer in context.Customers
                        on rental.CustomerId equals customer.Id
                    join car in context.Cars
                        on rental.CarId equals car.Id
                    join brand in context.Brands
                        on car.BrandId equals brand.Id
                    join colour in context.Colours
                        on car.ColourId equals colour.Id
                    join fuelType in context.Fuels
                        on car.FuelId equals fuelType.Id
                    join engineType in context.Engines
                        on car.EngineId equals engineType.Id
                    join carOwner in context.Customers
                        on car.UserId equals carOwner.Id
                    select new RentalDetailDto
                    {
                        Id = rental.Id,
                        BrandName = brand.Name,
                        ColourName = colour.Name,
                        CarOwner = carOwner.FirstName + " " + carOwner.LastName,
                        CustomerName = customer.FirstName + " " + customer.LastName,
                        FuelType = fuelType.Type,
                        RentDate = rental.RentDate,
                        EngineType = engineType.Type,
                        DoorNumber = car.DoorNumber,
                        FuelConsumption = car.FuelConsumption,
                        ModelYear = car.ModelYear,
                        DailyPrice = car.DailyPrice,
                        Description = car.Description,
                        Images = (from image in context.CarImages where image.CarId == car.Id select image.ImageUrl)
                            .ToList(),
                        ReturnDate = rental.ReturnDate
                    };
                return result.ToList();
            }
        }

        public List<RentalDetailDto> GetRentalDetailsByUserId(int userId)
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from rental in context.Rentals
                    join customer in context.Customers
                        on rental.CustomerId equals customer.Id
                    join car in context.Cars
                        on rental.CarId equals car.Id
                    join brand in context.Brands
                        on car.BrandId equals brand.Id
                    join colour in context.Colours
                        on car.ColourId equals colour.Id
                    join fuelType in context.Fuels
                        on car.FuelId equals fuelType.Id
                    join engineType in context.Engines
                        on car.EngineId equals engineType.Id
                    join carOwner in context.Customers
                        on car.UserId equals carOwner.Id
                    where rental.CustomerId == userId
                    select new RentalDetailDto
                    {
                        Id = rental.Id,
                        BrandName = brand.Name,
                        ColourName = colour.Name,
                        CarOwner = carOwner.FirstName + " " + carOwner.LastName,
                        CustomerName = customer.FirstName + " " + customer.LastName,
                        FuelType = fuelType.Type,
                        RentDate = rental.RentDate,
                        EngineType = engineType.Type,
                        DoorNumber = car.DoorNumber,
                        FuelConsumption = car.FuelConsumption,
                        ModelYear = car.ModelYear,
                        DailyPrice = car.DailyPrice,
                        Description = car.Description,
                        Images = (from image in context.CarImages where image.CarId == car.Id select image.ImageUrl)
                            .ToList(),
                        ReturnDate = rental.ReturnDate
                    };
                return result.ToList();
            }
        }

        public List<RentalDetailDto> GetDetailByCarId(int carId)
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from rental in context.Rentals
                    join customer in context.Customers
                        on rental.CustomerId equals customer.Id
                    join car in context.Cars
                        on rental.CarId equals car.Id
                    join brand in context.Brands
                        on car.BrandId equals brand.Id
                    join colour in context.Colours
                        on car.ColourId equals colour.Id
                    join fuelType in context.Fuels
                        on car.FuelId equals fuelType.Id
                    join engineType in context.Engines
                        on car.EngineId equals engineType.Id
                    join carOwner in context.Customers
                        on car.UserId equals carOwner.Id
                    where rental.CarId == carId
                    select new RentalDetailDto
                    {
                        Id = rental.Id,
                        BrandName = brand.Name,
                        ColourName = colour.Name,
                        CarOwner = carOwner.FirstName + " " + carOwner.LastName,
                        CustomerName = customer.FirstName + " " + customer.LastName,
                        FuelType = fuelType.Type,
                        RentDate = rental.RentDate,
                        EngineType = engineType.Type,
                        DoorNumber = car.DoorNumber,
                        FuelConsumption = car.FuelConsumption,
                        ModelYear = car.ModelYear,
                        DailyPrice = car.DailyPrice,
                        Description = car.Description,
                        Images = (from image in context.CarImages where image.CarId == car.Id select image.ImageUrl)
                            .ToList(),
                        ReturnDate = rental.ReturnDate
                    };
                return result.ToList();
            }
        }
    }
}