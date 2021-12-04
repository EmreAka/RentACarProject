using System;

namespace Core.Entities.Concrete
{
    public class Card
    {
        public int Id { get; set; }
        public string NameOnCard { get; set; }
        public string CardNumber { get; set; }
        public string Cvv { get; set; }
        public DateTime Expiration { get; set; }
    }
}