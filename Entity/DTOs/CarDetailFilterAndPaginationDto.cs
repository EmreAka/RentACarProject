using Core.Entities;

namespace Entity.DTOs
{
    public class CarDetailFilterAndPaginationDto: IDto
    {
        public int Page { get; set; }
        public string BrandName { get; set; }
        public string ColourName { get; set; }
    }
}