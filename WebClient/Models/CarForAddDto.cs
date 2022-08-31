using Microsoft.AspNetCore.Components.Forms;
using RentACarBlazorServer.Models;

namespace WebClient.Models;

public class CarForAddDto
{
    public Brand Brand { get; set; }
    public Colour Colour { get; set; }
    public Engine Engine { get; set; }
    public Fuel Fuel { get; set; }
    public int ModelYear { get; set; }
    public decimal DailyPrice { get; set; }
    public int FuelConsumption { get; set; }
    public int DoorNumber { get; set; }
    public string Description { get; set; }
    public List<IBrowserFile> Images { get; set; }
}
