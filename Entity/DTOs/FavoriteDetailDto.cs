using System.Collections.Generic;
using Core.Entities;

namespace Entity.DTOs
{
    public class FavoriteDetailDto: IDto
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string BrandName { get; set; }
        public string ColourName { get; set; }
        public string CompanyName { get; set; }
        public int ModelYear { get; set; }
        public decimal DailyPrice { get; set; }
        public string Description { get; set; }
        public List<string> Images { get; set; }
    }
}