using System;
using Core.Entities;

namespace Entity.Concrete
{
    public class Card : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string NameOnCard { get; set; }
        public string CardNumber { get; set; }
        public string Cvv { get; set; }
        public DateTime Expiration { get; set; }
    }
}