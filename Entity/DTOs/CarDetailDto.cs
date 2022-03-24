using Core.Entities;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs
{
    public class CarDetailDto : IDto
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public string ColourName { get; set; }
        public string CompanyName { get; set; }
        public string FuelType { get; set; }
        public string EngineType { get; set; }
        public int DoorNumber { get; set; }
        public int FuelConsumption { get; set; }
        public int ModelYear { get; set; }
        public decimal DailyPrice { get; set; }
        public string Description { get; set; }
        public List<string> Images { get; set; }
    }
}