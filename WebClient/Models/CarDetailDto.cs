namespace RentACarBlazorServer.Models
{
    public class CarDetailDto
    {

        public int Id { get; set; }
        public string? BrandName { get; set; }
        public string? ColourName { get; set; }
        public string? CompanyName { get; set; }
        public string? FuelType { get; set; }
        public string? EngineType { get; set; }
        public int DoorNumber { get; set; }
        public int FuelConsumption { get; set; }
        public int ModelYear { get; set; }
        public decimal DailyPrice { get; set; }
        public string? Description { get; set; }
        public List<string>? Images { get; set; }

        public override string ToString()
        {
            return $"Brand: {BrandName}, Colour: {ColourName}, " +
                $"Company: {CompanyName}, Fuel Type: {FuelType}, " +
                $"Engine Type: {EngineType}, Model: {ModelYear}, " +
                $"Daily Price: {DailyPrice}, Description: {Description}";
        }
    }
}
