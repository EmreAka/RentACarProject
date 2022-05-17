using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs
{
    public class CarForAddDto
    {
        public int UserId { get; set; }
        public int BrandId { get; set; }
        public int ColourId { get; set; }
        public int EngineId { get; set; }
        public int FuelId { get; set; }
        public int ModelYear { get; set; }
        public decimal DailyPrice { get; set; }
        public int FuelConsumption { get; set; }
        public int DoorNumber { get; set; }
        public string Description { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}