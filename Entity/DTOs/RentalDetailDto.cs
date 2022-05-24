using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs
{
    public class RentalDetailDto: IDto
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public string ColourName { get; set; }
        public string CarOwner { get; set; }
        public string CustomerName { get; set; }
        public string FuelType { get; set; }
        public string EngineType { get; set; }
        public int DoorNumber { get; set; }
        public int FuelConsumption { get; set; }
        public int ModelYear { get; set; }
        public decimal DailyPrice { get; set; }
        public string Description { get; set; }
        public List<string> Images { get; set; }
        
        public DateTime RentDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
