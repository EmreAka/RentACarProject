using Entity.Concrete;
using Card = Core.Entities.Concrete.Card;

namespace Entity.DTOs;

public class RentalWithCardInfoDto
{
    public Rental Rental { get; set; }
    public Card Card { get; set; }   
}